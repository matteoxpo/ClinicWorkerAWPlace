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
using Domain.Repositories;
using Domain.UseCases;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase, IRoutableViewModel
    {
        private string _UserLogin = "";
        private string _Password = "";

        
        
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
                    .WhenAnyValue(v => v.Password)
                    .Select(w => w.Length != 0)
                    .DistinctUntilChanged();

            var temp = isUserLoginValid.And(isUserPasswordValid).Then((b1, b2) => b1 && b2);
            _isDataValid = Observable.When(temp).ToProperty(this, h => h.IsDataValid);

            GoToWorkPlace = ReactiveCommand.CreateFromTask(TestData);

            _interactor = new DoctorEmployeeInteractor(DoctorEmployeeRepository.GetInstance());
        }
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        private async Task TestData()
        { 
            if (_interactor.AuthorizationUseCase(_UserLogin, _Password))
            {
            await HostScreen.Router.Navigate.Execute(new WorkPlaceViewModel(HostScreen));
            }

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
