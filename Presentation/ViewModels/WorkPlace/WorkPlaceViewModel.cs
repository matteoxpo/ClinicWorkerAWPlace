using System.Reactive;
using Data.Repositories;
using Domain.UseCases;
using Presentation.ViewModels.Login;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    private string _login;

    public WorkPlaceViewModel(IScreen hostScreen, string login)
    {
        _login = new string(login);

        Router = new RoutingState();

        IsUserDocotr = new UserEmployeeInteractor(UserEmployeeRepository.GetInstance()).IsUserDoctor(login);

        HostScreen = hostScreen;
        ListOfMedicinesViewModel = new ListOfMedicinesViewModel(this);
        DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this, login);
        WorkPlaceProfileViewModel = new WorkPlaceProfileViewModel(this, login);
        WorkPlaceHelpViewModel = new WorkPlaceHelpViewModel(this);
        DefaultWorkPlaceViewModel = new DefaultWorkPlaceViewModel(this, login);

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