using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.UseCases;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using Presentation.ViewModels.WorkPlace;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.Login;

public class LoginViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly UserEmployeeInteractor _userEmployeeInteractor;

    public LoginViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        GoToWorkPlace = ReactiveCommand.CreateFromTask(TryAuthorize);

        _userEmployeeInteractor =
            new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());
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
            if (UserLogin is null) throw new LoginViewModelException("Не введен логин");
            if (UserPassword is null) throw new LoginViewModelException("Не пароль");


            if (_userEmployeeInteractor.Authorization(UserLogin!, UserPassword!))
                await HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen, UserLogin!));
        }
        catch (Exception e)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Ошибка авторизации",
                    ContentMessage = e.Message
                });
            await messageBoxStandardWindow.Show();
        }
    }
}

public class LoginViewModelException : Exception
{
    public LoginViewModelException(string message) : base(message)
    {
    }
}