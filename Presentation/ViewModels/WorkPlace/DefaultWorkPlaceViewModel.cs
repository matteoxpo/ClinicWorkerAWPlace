using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel
    {
<<<<<<< HEAD
        public List<Client> Timetable { get; }
=======
        public List<Tuple<Client, DateTime>> Timetable { get; }
>>>>>>> temporary
        
        private DoctorEmployee self;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            HostScreen = hostScreen;
<<<<<<< HEAD
            Timetable = new List<Client>(self.Patients);
=======
            Timetable = new List<Tuple<Client, DateTime>>(self.Patients);
>>>>>>> temporary
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
    }
}
