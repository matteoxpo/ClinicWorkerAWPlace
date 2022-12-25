using System.Collections.Generic;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace;

public class ListOfMedicinesViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly MedicinesInteractor _medicinesInteractor;

    public ListOfMedicinesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _medicinesInteractor = new MedicinesInteractor(MedicinesRepository.GetInstance());
        Medicines = _medicinesInteractor.Get();
    }

    public IEnumerable<Medicines> Medicines { get; }
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    [Reactive] public Medicines SelectedDrug { get; set; }
}