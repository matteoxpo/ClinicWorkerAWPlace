using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace.Profile;

public partial class WorkPlaceProfileView : ReactiveUserControl<WorkPlaceProfileViewModel>
{
    public WorkPlaceProfileView()
    {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}