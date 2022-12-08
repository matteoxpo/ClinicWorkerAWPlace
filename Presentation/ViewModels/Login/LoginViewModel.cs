using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;
using ReactiveUI;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.ViewModels.Login
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _userLogin = "";
        private string _userPassword = "";
        public ReactiveCommand<Unit, Unit> GoToWorkPlace { get; }
        private ObservableAsPropertyHelper<bool> _isDataValid { get; }
        private bool IsDataValid => _isDataValid.Value;

        private DoctorEmployeeInteractor _interactor;
        
        public LoginViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

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

            GoToWorkPlace = ReactiveCommand.CreateFromTask(TestData);

            _interactor = new DoctorEmployeeInteractor(DoctorEmployeeRepository.GetInstance(),ClientRepository.GetInstance());
        }
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private async Task TestData()
        {
            DoctorEmployee doc;
            try
            {
                if ( _interactor.Authorization(UserLogin, UserPassword))
                {
                    await HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen, _interactor.Get(UserLogin)));
                }
            }
            catch (DoctorEmployeeException e)
            {
                Console.WriteLine(e);
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
