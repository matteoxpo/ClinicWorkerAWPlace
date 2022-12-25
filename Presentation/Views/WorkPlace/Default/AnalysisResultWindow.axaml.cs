using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.Views.WorkPlace.Default;

public partial class AnalysisResultWindow : ReactiveWindow<AnalysisResultViewModel>
{
    public AnalysisResultWindow()
    {
        InitializeComponent();
        // this.WhenActivated(d => { ViewModel!.Close.Subscribe(Close).DisposeWith(d); });

    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}