using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel
    {
        private List<Tuple<Client, DateTime>> Timetable { get; }

        public Interaction<AdditionPatientViewModel, Tuple<Client?, DateTime>> ShowAdditionPatient { get; }
        public ReactiveCommand<Unit, Unit> AddPatient { get; }

        public List<string> Clients { get; }

        private DoctorEmployee self;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            HostScreen = hostScreen;
            Timetable = new List<Tuple<Client, DateTime>>(self.Patients);
            Clients = new List<string>();

            ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Client?>();
            
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
                if (newPatien is not null)
                {
                    self.Patients.Add(newPatien);
                }
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
