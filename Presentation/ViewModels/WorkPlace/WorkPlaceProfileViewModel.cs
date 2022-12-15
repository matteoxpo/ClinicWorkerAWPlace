using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.UseCases;
using Microsoft.CodeAnalysis;

namespace Presentation.ViewModels.WorkPlace
{
    public class WorkPlaceProfileViewModel : ReactiveObject, IRoutableViewModel
    {
        private UserEmployeeInteractor _userEmployeeInteractor;
        private string _login;
        public WorkPlaceProfileViewModel(IScreen hostScreen, string login)
        {
            _login = new string(login);
            HostScreen = hostScreen;
            _userEmployeeInteractor = new UserEmployeeInteractor(UserEmployeeRepository.GetInstance());
            var t = _userEmployeeInteractor.Observe(login);
            t.Subscribe(d =>
            {
                Name = d.Name;
                Surname = d.Surname;
                Login = d.Login;
            });
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
    }
}
