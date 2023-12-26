using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Domain.Entities;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.Views.WorkPlace.Default;

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