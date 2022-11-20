using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;

namespace Presentation.ViewModels.WorkPlace
{
    public class WorkPlaceProfileViewModel : ViewModelBase, IRoutableViewModel
    {
        public string Name
        {
            get
            {
                return "asdas";
            }
        }


        public WorkPlaceProfileViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        // public RoutingState Router { get; } = new RoutingState();


    }
}
