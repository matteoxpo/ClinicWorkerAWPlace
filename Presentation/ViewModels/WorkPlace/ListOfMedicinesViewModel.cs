using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.Polyclinic.Drug;
using Domain.Repositories.Polyclinic;
using Domain.UseCases;
using Presentation.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace;

public class ListOfMedicinesViewModel : ReactiveObject, IRoutableViewModel
{
    private IDrugRepository drugRepository;
    private IDiseaseRepository diseaseRepository;
    public ListOfMedicinesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        drugRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetDrugRepository();
        diseaseRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetDiseaseRepository();

        var drugs = drugRepository.ReadAllAsync().GetAwaiter().GetResult();
        var drugList = new List<string>(drugs.Count);
        foreach (var drug in drugs)
        {
            drugList.Add(
                new string("Название: " + drug.Name +
                " \nОписание: " + drug.Description +
                " \nРецепт: " + drug.Recipe.ToString())
            );
        }

        var diseases = diseaseRepository.ReadAllAsync().GetAwaiter().GetResult();
        var diseasesList = new List<string>(diseases.Count);
        foreach (var disease in diseases)
        {
            diseasesList.Add(
                new string("Название: " + disease.Name +
                " \nОписание: " + disease.Description +
                " \nСпособ передачи: " + disease.Transmission.ToString())
            );
        }

        Diseases = diseasesList;
        Medicines = drugList;
    }
    public IEnumerable<string> Medicines { get; }
    public IEnumerable<string> Diseases { get; }
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}