using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Avalonia.Media.TextFormatting;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.UseCases;
using DynamicData;
using DynamicData.Binding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        public Interaction<AdditionPatientViewModel, Appointment?> ShowAdditionPatient { get; }
        public ReactiveCommand<Unit, Unit> AddAppointment { get; }
        public ReactiveCommand<Unit, Unit> AddPatient { get; }
        public IScreen HostScreen { get; }

        public string? UrlPathSegment { get; }
        
        public Client? SelectedClient
        {
            get => _selectedClient;
            set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
        }

        public string? SelectedClientNewAppointmentTime
        {
            get => _selectedClientNewAppointmentTime;
            set => this.RaiseAndSetIfChanged(ref _selectedClientNewAppointmentTime, value);
        }

        public string? SelectedClientNewAppointmentComplaints
        {
            get => _selectedClientNewAppointmentComplaints;
            set => this.RaiseAndSetIfChanged(ref _selectedClientNewAppointmentComplaints, value);
        }

        public ObservableCollection<Client>? Clients
        {
            get => _clients;
            set => this.RaiseAndSetIfChanged(ref _clients, value);
        }

        public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
        {
            Login = login;
   
            HostScreen = hostScreen;
            
            SelectedClient = null;

            SelectedClientNewAppointmentComplaints = null;

            
            
            Activator = new ViewModelActivator();
            
            _clientInteractor = new ClientInteractor(ClientRepository.GetInstance());

            _doctorInteractor = new DoctorInteractor(
                ClientRepository.GetInstance(),
                AppointmentRepository.GetInstance(),
                DoctorRepository.GetInstance()
            );


            new UserEmployeeInteractor(
                    UserEmployeeRepository.GetInstance())
                .IsUserDoctor(login);

            this.WhenActivated(compositeDisposable =>
                _doctorInteractor
                    .Observe(login)
                    .Subscribe(UpdateClients)
                    .DisposeWith(compositeDisposable)
            );
            UpdateClients(login);

            SelectedClient = Clients != null && Clients.Count > 0 ? Clients.First() : new Client();
            
            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Appointment?>();

            AddPatient = ReactiveCommand.CreateFromTask(OnAddExtraAppointment);

            AddAppointment = ReactiveCommand.Create(OnAddNextAppointment);

        }

        private async Task OnAddExtraAppointment()
        {
            var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(Login));
            if (newAppointment is not null)
            {
                _doctorInteractor.AddAppointmnet(newAppointment, true);
            }
        }

        private void OnAddNextAppointment()
        {
            try
            {
                if (SelectedClientNewAppointmentTime is null)
                {
                    throw new NullReferenceException();
                }
                if (SelectedClientNewAppointmentComplaints is null)
                {
                    throw new NullReferenceException();
                }
                var newAppointment = new Appointment(Login, SelectedClient.Id,
                    DateTime.Parse(SelectedClientNewAppointmentTime), SelectedClientNewAppointmentComplaints);
            }
            catch (Exception ex)
            {
                // ignored for a whiele
            }
        }

        private void UpdateClients(Doctor doctor)
        {
            Clients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctor.Login));
        }
        private void UpdateClients(string doctorLogin)
        {
            Clients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctorLogin));
        }



        private string  Login { get; }
        // private Doctor _doctor;

        private readonly DoctorInteractor _doctorInteractor;

        private readonly ClientInteractor _clientInteractor;
        // private readonly UserEmployeeInteractor _userEmployeeInteractor;
        
        private ObservableCollection<Client>? _clients;

        private Client? _selectedClient;
        public ViewModelActivator Activator { get; }



        private string? _selectedClientNewAppointmentTime;
        private string? _selectedClientNewAppointmentComplaints;
    }
}