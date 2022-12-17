using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace.Default;

namespace Presentation.Views.WorkPlace.Default;

public partial class ClientData : ReactiveUserControl<DefaultWorkPlaceViewModel>
{
    public ClientData()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}