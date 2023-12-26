using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Data.Repositories.App;
using Domain.Repositories.App;
using Presentation.Configuration;
using Presentation.ViewModels.WorkPlace;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.Login;

public class LoginViewModel : ViewModelBase, IRoutableViewModel
{
    private IAuthRepository _authRepository;

    public LoginViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        GoToWorkPlace = ReactiveCommand.CreateFromTask(TryAuthorize);

        _authRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAuthRepository();
    }

    public ReactiveCommand<Unit, Unit> GoToWorkPlace { get; }

    [Reactive] public string? UserLogin { get; set; }

    [Reactive] public string? UserPassword { get; set; }


    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    private async Task TryAuthorize()
    {
        try
        {
            if (UserLogin is null)
            {
                throw new LoginViewModelException("Не введен логин");
            }
            if (UserPassword is null)
            {
                throw new LoginViewModelException("Не пароль");
            }
            if (_authRepository.Auth(UserLogin!, UserPassword!))
            {

                // TODO: add menu to choose role
                await HostScreen.Router.Navigate.Execute(
                    new WorkPlaceViewModel(HostScreen, await _authRepository.GetIdByLoginAsync(UserLogin))
                );
            }
        }
        catch (LoginViewModelException e)
        {
            await ShowExceptionMessageBox(e);
        }
        catch (Exception e)
        {
            await ShowUncatchedExceptionMessageBox(e);
        }
    }
}

public class LoginViewModelException : Exception
{
    public LoginViewModelException(string message) : base(message)
    {
    }
}