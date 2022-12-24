using Presentation.ViewModels.Login;
using ReactiveUI;

namespace Presentation.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public MainWindowViewModel()
    {
        Router = new RoutingState();
        LoginViewModel = new LoginViewModel(this);
    }

    public LoginViewModel LoginViewModel { get; }
    public RoutingState Router { get; }
}