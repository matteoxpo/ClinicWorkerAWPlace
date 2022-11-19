using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using Domain.Common.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class DefaultWorkPlaceViewModel : ViewModelBase, IRoutableViewModel
    {
        public List<Client> Timetable { get; }

        public DefaultWorkPlaceViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            Client temp = new("Sereja", "Khromin", new DateTime(123));
            Timetable = new List<Client>();
            Timetable.Add(temp);
            Timetable.Add(temp);
        }
        public IScreen HostScreen { get; }
        public string? UrlPathSegment { get; }
    }
}
