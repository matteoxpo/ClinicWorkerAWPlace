namespace Domain.Entities;

[Serializable]
public class Medicines
{
    public string Title { get; set; }
    public string IndicationsForUse{ get; set; }
    public string Manufacturer{ get; set; }
    public bool CanBeSoldWithoutPrescription{ get; set; }


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
}