using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        private List<Tuple<Client, DateTime>> Timetable { get; set; }
        public Interaction<AdditionPatientViewModel, Tuple<Client?, DateTime>?> ShowAdditionPatient { get; }
        public ReactiveCommand<Unit, Unit> AddPatient { get; }
        private DoctorEmployeeInteractor _interactor;
        private IObservable<DoctorEmployee> _observable;
        public List<string> Clients { get; }


        private string _login;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
        {
            _login = login;
            
            HostScreen = hostScreen;

            Activator = new ViewModelActivator();

            _interactor = new DoctorEmployeeInteractor(DoctorEmployeeRepository.GetInstance(), ClientRepository.GetInstance());

            _observable = _interactor.Observe(login);
            
            Timetable = new List<Tuple<Client, DateTime>>(_interactor.Get(login).Patients);
            
            var q = _observable.Subscribe((d) => Timetable = new List<Tuple<Client, DateTime>>(d.Patients));
            this.WhenActivated(r => q.DisposeWith(r));
            
            Clients = new List<string>();

            
            foreach (var el in Timetable)
            {
                Clients.Add(
                    el.Item2.ToString(CultureInfo.InvariantCulture) + 
                    "  " + el.Item1.Name + 
                    "  " + el.Item1.Surname);
            }

            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Tuple<Client?, DateTime>?>();
            AddPatient = ReactiveCommand.CreateFromTask(OnAddPatient);
            
            // this.WhenActivated((d) => d.Subscribe((h) => Timetable = new List<Tuple<Client, DateTime>>(h.Patients)).DisposeWith(d));
            
        }

        private async Task OnAddPatient()
        {
            try
            {
                var newPatien = await ShowAdditionPatient.Handle(new AdditionPatientViewModel());
                _interactor.AddPatient(_login, new Client(), new DateTime(2023, 12, 12)); 
            }
            catch (Exception e)
            {
                //
            }
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
        public ViewModelActivator Activator { get; }
    }
}
