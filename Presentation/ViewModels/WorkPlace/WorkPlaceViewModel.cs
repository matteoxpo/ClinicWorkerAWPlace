using System.Reactive;
using Data.Repositories;
using Domain.Entities.App.Role.Employees;
using Presentation.Configuration;
using Presentation.ViewModels.Login;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    private int _userId;

    public bool IsDoctor { get; }
    public bool IsRegistrar { get; }

    public WorkPlaceViewModel(IScreen hostScreen, int userId)
    {
        _userId = userId;

        Router = new RoutingState();
        IsDoctor = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAuthRepository().IsUser<Doctor>(userId);
        IsRegistrar = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAuthRepository().IsUser<Registrar>(userId);

        HostScreen = hostScreen;
        ListOfMedicinesViewModel = new ListOfMedicinesViewModel(this);
        WorkPlaceProfileViewModel = new WorkPlaceProfileViewModel(this, userId);
        WorkPlaceHelpViewModel = new WorkPlaceHelpViewModel(this);

        DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this, userId);
        DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this, userId);

        GoListOfMedicines = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(ListOfMedicinesViewModel));

        GoToProfile = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(WorkPlaceProfileViewModel));

        GoToDefault = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(DefaultWorkPlaceViewModel));

        GoToHelp = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(WorkPlaceHelpViewModel));

        LogOut = ReactiveCommand.CreateFromObservable(
            () => HostScreen.Router.Navigate.Execute(new LoginViewModel(HostScreen)));
    }

    public ReactiveCommand<Unit, IRoutableViewModel> GoToProfile { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoListOfMedicines { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToDefault { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToHelp { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> LogOut { get; }
    private DefaultWorkPlaceViewModel DefaultWorkPlaceViewModel { get; }
    private WorkPlaceProfileViewModel WorkPlaceProfileViewModel { get; }

    private ListOfMedicinesViewModel ListOfMedicinesViewModel { get; }
    private WorkPlaceHelpViewModel WorkPlaceHelpViewModel { get; }
    public bool IsUserDocotr { get; }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}