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

namespace Presentation.ViewModels.Login;

public class LoginViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly UserEmployeeInteractor _userEmployeeInteractor;
    private string? _userLogin;
    private string? _userPassword;

    public LoginViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        GoToWorkPlace = ReactiveCommand.CreateFromTask(TryAuthorize);

        _userEmployeeInteractor =
            new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());
    }

    public ReactiveCommand<Unit, Unit> GoToWorkPlace { get; }

    public string? UserLogin
    {
        get => _userLogin;
        set => this.RaiseAndSetIfChanged(ref _userLogin, value);
    }

    public string? UserPassword
    {
        get => _userPassword;
        set => this.RaiseAndSetIfChanged(ref _userPassword, value);
    }


    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    private async Task TryAuthorize()
    {
        try
        {
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