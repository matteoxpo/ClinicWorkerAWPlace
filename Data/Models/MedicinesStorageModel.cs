using Data.Models;

namespace Domain.Entities;

[Serializable]
public class MedicinesStorageModel : IConverter<Medicines, MedicinesStorageModel>
{
    public MedicinesStorageModel(string title, string indicationsForUse, string manufacturer, bool canBeSoldWithoutPrescription)
    {
        Title = title;
        IndicationsForUse = indicationsForUse;
        Manufacturer = manufacturer;
        CanBeSoldWithoutPrescription = canBeSoldWithoutPrescription;
    }

    public MedicinesStorageModel()
    {
        Title = new string("title");
        IndicationsForUse = new string("indications");
        Manufacturer = new string("manuf");
    }

    public string Title { get; set; }
    public string IndicationsForUse { get; set; }
    public string Manufacturer { get; set; }
    public bool CanBeSoldWithoutPrescription { get; set; }

    public override string ToString()
    {
        return string.Join("\n",
            "Название: " + Title,
            "Применятеся при:\n" + IndicationsForUse,
            CanBeSoldWithoutPrescription ? "Продается без рецепта" : "Без рецепта не продается",
            "Производитель " + Manufacturer);
    }

    public Medicines ConvertToEntity(MedicinesStorageModel entity)
    {
        return new Medicines(entity.Title, entity.IndicationsForUse, entity.Manufacturer, entity.CanBeSoldWithoutPrescription);
    }

    public MedicinesStorageModel ConvertToStorageEntity(Medicines entity)
    {
        return new MedicinesStorageModel(entity.Title, entity.IndicationsForUse, entity.Manufacturer, entity.CanBeSoldWithoutPrescription);

    }
}