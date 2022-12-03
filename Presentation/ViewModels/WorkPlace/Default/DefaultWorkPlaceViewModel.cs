using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel
    {
        //
        // приноси данные по лоигу сюда прокидывай логин через интерактор все остальное получай
        // 
        private List<Tuple<Client, DateTime>> Timetable { get; set; }
        public Interaction<AdditionPatientViewModel, Tuple<Client?, DateTime>?> ShowAdditionPatient { get; }
        public ReactiveCommand<Unit, Unit> AddPatient { get; }
        private DoctorEmployeeInteractor _interactor;
        public List<string> Clients { get; }

        private DoctorEmployee self;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
        {

            _interactor = new DoctorEmployeeInteractor(DoctorEmployeeRepository.GetInstance());

            // get login from wp
            // string login = new string("asdas");
            var t = _interactor.Observe(login);
             t.Subscribe((d) => Timetable = new List<Tuple<Client, DateTime>>(d.Patients)).Dispose();
            //
            
            HostScreen = hostScreen;
            
            // Timetable = new List<Tuple<Client, DateTime>>(self.Patients);
            Clients = new List<string>();

            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Tuple<Client?, DateTime>?>();
            
            foreach (var el in Timetable)
            {
                Clients.Add(
                    el.Item2.ToString(CultureInfo.InvariantCulture) + 
                    "  " + el.Item1.Name + 
                    "  " + el.Item1.Surname);
            }

            AddPatient = ReactiveCommand.CreateFromTask(OnAddPatient);


        }

        private async Task OnAddPatient()
        {
            try
            {
                var newPatien = await ShowAdditionPatient.Handle(new AdditionPatientViewModel());
                // self.Patients.Add(newPatien!);
                // _interactor.AddPatienUseCae(self, newPatient);
            }
            catch (Exception e)
            {
                //
            }
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
    }
}
