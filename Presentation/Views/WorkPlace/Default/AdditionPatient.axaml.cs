using System;
using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace.Default;
using ReactiveUI;

namespace Presentation.Views.WorkPlace.Default;

public partial class AdditionPatientWindow : ReactiveWindow<AdditionPatientViewModel>
{
    public AdditionPatientWindow()
    {
        this.WhenActivated(d => { ViewModel!.AddPatient.Subscribe(Close).DisposeWith(d); });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}