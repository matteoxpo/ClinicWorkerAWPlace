using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel
    {
        public List<Tuple<Client, DateTime>> Timetable { get; }
        
        private DoctorEmployee self;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            HostScreen = hostScreen;
            Timetable = new List<Tuple<Client, DateTime>>(self.Patients);
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
    }
}
