using System;
using Presentation.ViewModels.Login;
using Presentation.ViewModels.WorkPlace;
using Presentation.ViewModels.WorkPlace.Default;
using Presentation.Views.Login;
using Presentation.Views.WorkPlace;
using Presentation.Views.WorkPlace.Default;
using Presentation.Views.WorkPlace.Help;
using Presentation.Views.WorkPlace.ListOfMedecines;
using Presentation.Views.WorkPlace.Profile;
using ReactiveUI;

namespace Presentation;

public class AppViewLocator : IViewLocator

{
    public IViewFor ResolveView<T>(T viewModel, string contract = null)
    {
        return viewModel switch
        {
            LoginViewModel context => new LoginView { DataContext = context },

            DefaultWorkPlaceViewModel context => new DefaultWorkPlaceView { DataContext = context },

            WorkPlaceViewModel context => new WorkPlaceView { DataContext = context },

            WorkPlaceProfileViewModel context => new WorkPlaceProfileView { DataContext = context },

            WorkPlaceHelpViewModel context => new WorkPlaceHelpView { DataContext = context },

            ListOfMedicinesViewModel context => new ListOfMedicinesView { DataContext = context },

            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}