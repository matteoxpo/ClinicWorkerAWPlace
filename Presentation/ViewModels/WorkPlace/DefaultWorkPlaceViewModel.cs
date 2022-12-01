using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel
    {
        public List<Client> Timetable { get; }
        
        private DoctorEmployee self;

        public DefaultWorkPlaceViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            HostScreen = hostScreen;
            Timetable = new List<Client>(self.Patients);
        }

        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
    }
}
