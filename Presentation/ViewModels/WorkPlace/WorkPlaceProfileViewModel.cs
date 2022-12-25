using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceProfileViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
{
    private readonly UserEmployeeInteractor _userEmployeeInteractor;

    public WorkPlaceProfileViewModel(IScreen hostScreen, string login)
    {
        Login = login;
        Activator = new ViewModelActivator();
        HostScreen = hostScreen;
        _userEmployeeInteractor = new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());
        UserEmployee = _userEmployeeInteractor.Get(login);
        this.WhenActivated(compositeDisposable =>
            _userEmployeeInteractor
                .Observe(login)
                .Subscribe(UpdateUser)
                .DisposeWith(compositeDisposable)
        );
        ChangePassword = ReactiveCommand.Create(OnChangePassword);

        IsDataInvalid = false;
    }

    private string Login { get; }

    [Reactive] public UserEmployee UserEmployee { get; set; }

    [Reactive] public string OldPassword { get; set; }

    [Reactive] public string NewPassword { get; set; }
    public ReactiveCommand<Unit, Unit> ChangePassword { get; }

    [Reactive] public bool IsDataInvalid { get; set; }

    [Reactive] public string ErrorMessage { get; set; }

    [Reactive] public bool IsPasswordChanged { get; set; }

    [Reactive] public bool IsActionOn { get; set; }

    public ViewModelActivator Activator { get; }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    private void OnChangePassword()
    {
        try
        {
            _userEmployeeInteractor.ChangePassword(Login, OldPassword, NewPassword);
            IsDataInvalid = false;
            IsPasswordChanged = true;
            NewPassword = OldPassword = string.Empty;
            var observable = Observable.Return(Unit.Default);
            var delay = observable.Delay(TimeSpan.FromSeconds(3));
            delay.SubscribeOn(RxApp.MainThreadScheduler);

            delay.Subscribe(_ => IsActionOn = false);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            IsPasswordChanged = false;
            IsDataInvalid = true;
        }

        IsActionOn = true;
    }

    public void UpdateUser(UserEmployee userEmployee)
    {
        UserEmployee = userEmployee;
    }
}