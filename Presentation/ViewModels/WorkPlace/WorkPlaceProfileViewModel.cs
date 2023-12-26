using System;
using System.Reactive;
using System.Reactive.Linq;
using Domain.Repositories.App;
using Presentation.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace;

public class WorkPlaceProfileViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
{
    private IAuthRepository authRepository;
    private int id;
    public WorkPlaceProfileViewModel(IScreen hostScreen, int id)
    {
        this.id = id;

        authRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAuthRepository();

        Activator = new ViewModelActivator();

        HostScreen = hostScreen;

        ChangePassword = ReactiveCommand.Create(OnChangePassword);

        IsDataInvalid = false;
    }

    private string Login { get; }

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

    private async void OnChangePassword()
    {
        try
        {
            await authRepository.ResetPasswordAsync(NewPassword, id);
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
}