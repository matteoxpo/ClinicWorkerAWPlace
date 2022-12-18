using System.Collections.Generic;
using Data.Repositories;
using Domain.Entities;
using Domain.UseCases;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace;

public class ListOfMedicinesViewModel : ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public IEnumerable<Medicines> Medicines { get; }

    private readonly MedicinesInteractor _medicinesInteractor;
    public ListOfMedicinesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _medicinesInteractor = new MedicinesInteractor(MedicinesRepository.GetInstance());
        Medicines = _medicinesInteractor.Get();

    }
}