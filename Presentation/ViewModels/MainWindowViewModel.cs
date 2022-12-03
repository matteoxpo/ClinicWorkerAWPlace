using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Input;
using ReactiveUI;
using System;
using Presentation.ViewModels.Login;

namespace Presentation.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }
        public LoginViewModel LoginViewModel { get; }

        public MainWindowViewModel()
        {
            Router = new RoutingState();
            LoginViewModel = new LoginViewModel(this);
        }
    }
}
