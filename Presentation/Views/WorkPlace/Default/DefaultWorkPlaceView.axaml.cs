using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Domain.Entities;
using Domain.Entities.Roles.Doctor;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.Views.WorkPlace.Default;

public partial class DefaultWorkPlaceView : ReactiveUserControl<DefaultWorkPlaceViewModel>
{
    public DefaultWorkPlaceView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
            ViewModel!.ShowAdditionPatient
                .RegisterHandler(DoShowAdditionPatient)
                .DisposeWith(d)
        );
        this.WhenActivated(d =>
            ViewModel!.ShowAnalysisResult
                .RegisterHandler(DoShowAnalyResult)
                .DisposeWith(d)
        );
    }

    private async Task DoShowAdditionPatient(
        InteractionContext<AdditionPatientViewModel, Appointment?> interactionContext)
    {
        var dialog = new AdditionPatientWindow
        {
            DataContext = interactionContext.Input
        };

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow != null)
            {
                var newAppoitment = await dialog.ShowDialog<Appointment>(desktop.MainWindow);
                interactionContext.SetOutput(newAppoitment);
            }
        }
        else
        {
            interactionContext.SetOutput(null);
        }
    }

    private async Task DoShowAnalyResult(
        InteractionContext<AnalysisResultViewModel, Unit> interactionContext)
    {
        var dialog = new AnalysisResultWindow()
        {
            ViewModel = interactionContext.Input
        };

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow != null)
            {
                await dialog.ShowDialog<Unit>(desktop.MainWindow);
            }
        }
        interactionContext.SetOutput(Unit.Default);
    }

    public void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}