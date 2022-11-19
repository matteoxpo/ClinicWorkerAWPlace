using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace
{
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
}