using System;
using Presentation.ViewModels;
using Presentation.ViewModels.WorkPlace;
using Presentation.ViewModels.Login;
using Presentation.Views;
using Presentation.Views.WorkPlace;
using Presentation.Views.WorkPlace.Default;
using Presentation.Views.WorkPlace.Help;
using Presentation.Views.WorkPlace.Profile;
using Presentation.Views.Login;


using ReactiveUI;

namespace Presentation
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
        {
            LoginViewModel context => new LoginView { DataContext = context },

            DefaultWorkPlaceViewModel context => new DefaultWorkPlaceView { DataContext = context },

            WorkPlaceViewModel context => new WorkPlaceView { DataContext = context },

            WorkPlaceProfileViewModel context => new WorkPlaceProfileView { DataContext = context },

            WorkPlaceHelpViewModel context => new WorkPlaceHelpView { DataContext = context },

            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

