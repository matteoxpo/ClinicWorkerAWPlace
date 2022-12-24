using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceProfileViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
{
    private readonly UserEmployeeInteractor _userEmployeeInteractor;
    private string _errorMessage;

    private bool _isActionOn;

    private bool _isDataInvalid;

    private bool _isPasswordChanged;
    private readonly string _login;

    private string _newPassword;
    private string _oldPassword;
    private UserEmployee _userEmployee;

    public WorkPlaceProfileViewModel(IScreen hostScreen, string login)
    {
        _login = login;
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

    public UserEmployee UserEmployee
    {
        get => _userEmployee;
        set => this.RaiseAndSetIfChanged(ref _userEmployee, value);
    }

    public string OldPassword
    {
        get => _oldPassword;
        set => this.RaiseAndSetIfChanged(ref _oldPassword, value);
    }

    public string NewPassword
    {
        get => _newPassword;
        set => this.RaiseAndSetIfChanged(ref _newPassword, value);
    }

    public ReactiveCommand<Unit, Unit> ChangePassword { get; }

    public bool IsDataInvalid
    {
        get => _isDataInvalid;
        set => this.RaiseAndSetIfChanged(ref _isDataInvalid, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool IsPasswordChanged
    {
        get => _isPasswordChanged;
        set => this.RaiseAndSetIfChanged(ref _isPasswordChanged, value);
    }

    public bool IsActionOn
    {
        get => _isActionOn;
        set => this.RaiseAndSetIfChanged(ref _isActionOn, value);
    }

    public ViewModelActivator Activator { get; }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    private void OnChangePassword()
    {
        try
        {
            _userEmployeeInteractor.ChangePassword(_login, OldPassword, NewPassword);
            IsDataInvalid = false;
            IsPasswordChanged = true;
            NewPassword = OldPassword = new string("");
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