using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Domain.Entities.People;
using Presentation.ViewModels.WorkPlace;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.Views.WorkPlace.Default
{
    public partial class DefaultWorkPlaceView : ReactiveUserControl<DefaultWorkPlaceViewModel>
    {
        public DefaultWorkPlaceView()
        {
            this.WhenActivated(d =>
            {
                ViewModel!.ShowAdditionPatient
                    .RegisterHandler(DoShowAdditionPatient)
                    .DisposeWith(d);
            });
            InitializeComponent();
        }

        private async Task DoShowAdditionPatient(InteractionContext<AdditionPatientViewModel, Tuple<Client?, DateTime>?> interactionContext)
        {
            var dialog = new AdditionPatientWindow()
            {
                DataContext = interactionContext.Input
            };
            
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var newClient = await dialog.ShowDialog<Tuple<Client?, DateTime>?>(desktop.MainWindow);
                interactionContext.SetOutput(newClient);
            }
            else
            {
                interactionContext.SetOutput(null); 
            }
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}