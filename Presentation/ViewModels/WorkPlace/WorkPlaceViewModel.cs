using System;
using System.Reactive;
using ReactiveUI;
using Presentation.ViewModels.Login;
using System.Collections.Generic;
using Domain.Entities.People;
using Presentation.ViewModels.WorkPlace.Default;

namespace Presentation.ViewModels.WorkPlace
{
    public class WorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public RoutingState Router { get; } 

        public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToDefault { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToHelp { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> LogOut { get; }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private string _login;
        DefaultWorkPlaceViewModel DefaultWorkPlaceViewModel { get; }

        public WorkPlaceViewModel(IScreen hostScreen, string login)
        {
            _login = new string(login);
            
            Router = new RoutingState();
            
            HostScreen = hostScreen;

            DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this, login);

            GoToProfile = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new WorkPlaceProfileViewModel(this, login)));

            GoToDefault = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new DefaultWorkPlaceViewModel(this, login)));

            GoToHelp = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new WorkPlaceHelpViewModel(this)));

            LogOut = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new LoginViewModel(HostScreen)));

        }

    }
}
