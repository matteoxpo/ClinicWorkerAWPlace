using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia.Controls;
using Data.Repositories;
using Domain.UseCases;
using MessageBox.Avalonia.DTO;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.ViewModels.Login
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _userLogin;
        private string _userPassword;
        public ReactiveCommand<Unit, Unit> GoToWorkPlace { get; }
        private ObservableAsPropertyHelper<bool> _isDataValid { get; }
        private bool IsDataValid => _isDataValid.Value;

        private UserEmployeeInteractor _userEmployeeInteractor;
        
        public LoginViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            _userLogin = new string("");
            _userPassword = new string("");
            
            var isUserLoginValid = this
                    .WhenAnyValue(v => v.UserLogin)
                    .Select(w => w.Length != 0)
                    .DistinctUntilChanged();

            var isUserPasswordValid = this
                    .WhenAnyValue(v => v.UserPassword)
                    .Select(w => w.Length != 0)
                    .DistinctUntilChanged();

            var temp = isUserLoginValid.And(isUserPasswordValid).Then((b1, b2) => b1 && b2);
            _isDataValid = Observable.When(temp).ToProperty(this, h => h.IsDataValid);

            GoToWorkPlace = ReactiveCommand.CreateFromTask(TryAuthorize);

            _userEmployeeInteractor =
                new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());

        }
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private async Task TryAuthorize()
        {
            try
            {
                if ( _userEmployeeInteractor.Authorization(UserLogin, UserPassword))
                {
                    await HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen, UserLogin));
                }
            }
            catch (UserEmployeeException e)
            {
               var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                    new MessageBoxStandardParams
                    {
                        ContentTitle = "Ошибка авторизации",
                        ContentMessage = e.Message,
                        // WindowIcon = new WindowIcon("icon-rider.png")
                    });
            await messageBoxStandardWindow.Show();
                
            }

        }

        public string UserLogin
        {
            get => _userLogin;
            set => this.RaiseAndSetIfChanged(ref _userLogin, value);
        }
        public string UserPassword
        {
            get => _userPassword;
            set => this.RaiseAndSetIfChanged(ref _userPassword, value);
        }


    }
}
