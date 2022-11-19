using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace
{
    public partial class WorkPlaceView : ReactiveUserControl<WorkPlaceViewModel>
    {
        public WorkPlaceView()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}