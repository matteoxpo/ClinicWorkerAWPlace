using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace.Help;

public partial class WorkPlaceHelpView : ReactiveUserControl<WorkPlaceHelpViewModel>
{
    public WorkPlaceHelpView()
    {
        InitializeComponent();
    }

    public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}