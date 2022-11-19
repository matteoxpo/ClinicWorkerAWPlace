using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.Login;

namespace Presentation.Views.Login
{
    public partial class LoginView : ReactiveUserControl<LoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}