using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;
using ReactiveUI;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using Presentation.ViewModels.WorkPlace;
using Data.Db;

namespace Presentation.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase, IRoutableViewModel
    {
        // BaseConnectionWithDB myDb;
        private string _UserLogin = "";
        private string _Password = "";
        public ReactiveCommand<Unit, Unit> GoToWorkPlace { get; }

        // private ObservableAsPropertyHelper<bool> _isUserLoginValid { get; }
        // private bool IsUserLoginValid => _isUserLoginValid.Value;

        // private ObservableAsPropertyHelper<bool> _isUserPasswordValid { get; }
        // private bool IsUserPasswordValid => _isUserPasswordValid.Value;

        private ObservableAsPropertyHelper<bool> _isDataValid { get; }
        private bool IsDataValid => _isDataValid.Value;

        public LoginViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            // myDb = new BaseConnectionWithDB("C:\\Users\\s-hro\\source\\repos\\Presentation\\Data\\Db\\EmployeeDataStore.xml");

            var isUserLoginValid = this
                    .WhenAnyValue(v => v.UserLogin)
                    .Select(w => w.Length != 0)
                    .DistinctUntilChanged();

            var isUserPasswordValid = this
                    .WhenAnyValue(v => v.Password)
                    .Select(w => w.Length != 0)
                    .DistinctUntilChanged();

            var temp = isUserLoginValid.And(isUserPasswordValid).Then((b1, b2) => b1 && b2);
            _isDataValid = Observable.When(temp).ToProperty(this, h => h.IsDataValid);


            // GoToWorkPlace = ReactiveCommand.CreateFromObservable(
            //         () => HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen)));


            GoToWorkPlace = ReactiveCommand.CreateFromTask(TestData);

            // GoToWorkPlace = ReactiveCommand.CreateFromTask()
        }
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private async Task TestData()
        {
            // if (myDb.IsFileOpened)
            //     if (myDb.FindUserByLoginPassword(UserLogin, Password))
            await HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen));

        }

        public string UserLogin
        {
            get => _UserLogin;
            set => this.RaiseAndSetIfChanged(ref _UserLogin, value);
        }
        public string Password
        {
            get => _Password;
            set => this.RaiseAndSetIfChanged(ref _Password, value);
        }


    }
}
