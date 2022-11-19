using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace.Default
{
    public partial class DefaultWorkPlaceView : ReactiveUserControl<DefaultWorkPlaceViewModel>
    {
        public DefaultWorkPlaceView()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}