namespace Domain.Entities;

[Serializable]
public class Medicines
{
    public Medicines(string title, string indicationsForUse, string manufacturer, bool canBeSoldWithoutPrescription)
    {
        Title = title;
        IndicationsForUse = indicationsForUse;
        Manufacturer = manufacturer;
        CanBeSoldWithoutPrescription = canBeSoldWithoutPrescription;
    }

    public Medicines()
    {
        Title = new string("title");
        IndicationsForUse = new string("indications");
        Manufacturer = new string("manuf");
    }

    public string Title { get;  }
    public string IndicationsForUse { get; }
    public string Manufacturer { get; }
    public bool CanBeSoldWithoutPrescription { get; }

    public override string ToString()
    {
        return string.Join("\n",
            "Название: " + Title,
            "Применятеся при:\n" + IndicationsForUse,
            CanBeSoldWithoutPrescription ? "Продается без рецепта" : "Без рецепта не продается",
            "Производитель " + Manufacturer);
    }
}