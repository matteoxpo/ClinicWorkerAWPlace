using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presentation.ViewModels.WorkPlace;

namespace Presentation.Views.WorkPlace.ListOfMedecines;

public partial class ListOfMedicinesView : ReactiveUserControl<ListOfMedicinesViewModel>
{
    public ListOfMedicinesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}