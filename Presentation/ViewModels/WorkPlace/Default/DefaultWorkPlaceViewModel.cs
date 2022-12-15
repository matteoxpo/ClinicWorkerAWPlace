using System;
using System.Collections.Generic;
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
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        public Interaction<AdditionPatientViewModel, Appointment> ShowAdditionPatient { get; }
        public ReactiveCommand<Unit, Unit> AddPatient { get; }
        
        private DoctorInteractor _doctorInteractor;
        private ClientInteractor _clientInteractor;
        private UserEmployeeInteractor _userEmployeeInteractor;

        private Doctor _doctor;

        private List<Client> _clients;
        public List<string> Timetable { get; }

        private string _login;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
        {
            _login = login;
            
            HostScreen = hostScreen;

            _clientInteractor = new ClientInteractor(ClientRepository.GetInstance());
            _userEmployeeInteractor = new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());
            _doctorInteractor = new DoctorInteractor(ClientRepository.GetInstance(),
                AppointmentRepository.GetInstance(), DoctorRepository.GetInstance());

            _doctor = _doctorInteractor.Get(login);
            _clients =new List<Client>();
            Timetable = new List<string>();
            foreach (var appointment in _doctor.Appointments)
            {
                _clients.Add(_clientInteractor.Get(appointment.ClientId));
                Timetable.Add( _clients.Last().Surname + " " + _clients.Last().Name + " " + appointment.MeetTime );
            }
            
            Activator = new ViewModelActivator();
    
            // var q = _observable.Subscribe((d) => Timetable = new List<Tuple<Client, DateTime>>(d.Patients));
            // this.WhenActivated(r => q.DisposeWith(r));
            
            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Appointment>();
            AddPatient = ReactiveCommand.CreateFromTask(OnAddAppointment);

            IsUserDoctor = _userEmployeeInteractor.IsUserDoctor(login);

        }

        private async Task OnAddAppointment()
        {
            try
            {
                var newPatien = await ShowAdditionPatient.Handle(new AdditionPatientViewModel());
                // _doctorInteractor.
            }
            catch (Exception e)
            {
            }
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
        public ViewModelActivator Activator { get; }
        public bool IsUserDoctor { get; }
    }
}
