using System;
using System.Reactive;
using ReactiveUI;
using Presentation.ViewModels.Login;
using System.Collections.Generic;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class WorkPlaceViewModel : ViewModelBase, IRoutableViewModel, IScreen
    {
        private DoctorEmployee self;
        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
        //public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToDefault { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToHelp { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> LogOut { get; }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        DefaultWorkPlaceViewModel DefaultWorkPlaceViewModel { get; }

        public WorkPlaceViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            
            HostScreen = hostScreen;

            DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this);

            GoToProfile = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new WorkPlaceProfileViewModel(this)));

            GoToDefault = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new DefaultWorkPlaceViewModel(this)));

            GoToHelp = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new WorkPlaceHelpViewModel(this)));

            LogOut = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new LoginViewModel(HostScreen)));

        }

    }
}
