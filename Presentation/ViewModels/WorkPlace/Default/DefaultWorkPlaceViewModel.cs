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
        public ReactiveCommand<Unit, Unit> AddPatient { get; }
        public IScreen HostScreen { get; }

        public string? UrlPathSegment { get; }
        public bool IsUserDoctor { get; }

        public Client SelectedClient
        {
            get => _selectedClient;
            set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
        }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => this.RaiseAndSetIfChanged(ref _clients, value);
        }

        private ObservableCollection<Client> _clients;


        public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
        {
            _login = login;
            HostScreen = hostScreen;
            
            
            Activator = new ViewModelActivator();

            _doctorInteractor = new DoctorInteractor(
                ClientRepository.GetInstance(),
                AppointmentRepository.GetInstance(),
                DoctorRepository.GetInstance()
            );

            IsUserDoctor = new UserEmployeeInteractor(
                UserEmployeeRepository.GetInstance())
                .IsUserDoctor(login);

            this.WhenActivated(compositeDisposable =>
                _doctorInteractor
                    .Observe(login)
                    .Subscribe(UpdateClients)
                    .DisposeWith(compositeDisposable)
            );

            Clients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(_login));

            SelectedClient = Clients.Count > 0 ? Clients.First() : new Client();
            
            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Appointment?>();

            AddPatient = ReactiveCommand.CreateFromTask(OnAddAppointment);
        }

        private async Task OnAddAppointment()
        {
            var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(_login));
            if (newAppointment is not null)
            {
                _doctorInteractor.AddAppointmnet(newAppointment);
            }
        }

        private void UpdateClients(Doctor doctor)
        {
            Clients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctor.Login));
        }

        private string _login;
        // private Doctor _doctor;

        private readonly DoctorInteractor _doctorInteractor;

        // private readonly ClientInteractor _clientInteractor;
        // private readonly UserEmployeeInteractor _userEmployeeInteractor;

        private Client _selectedClient;
        public ViewModelActivator Activator { get; }
    }
}