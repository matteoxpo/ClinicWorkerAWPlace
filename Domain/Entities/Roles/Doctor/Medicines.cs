namespace Domain.Entities.Roles.Doctor;

public class Medicines
{
    public Medicines(string title, string indicationsForUse, string manufacturer, bool canBeSoldWithoutPrescription)
    {
        Title = title;
        IndicationsForUse = indicationsForUse;
        Manufacturer = manufacturer;
        CanBeSoldWithoutPrescription = canBeSoldWithoutPrescription;
    }

    public string Title { get; }
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