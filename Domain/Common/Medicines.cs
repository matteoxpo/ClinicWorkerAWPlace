
namespace Domain.Common;

public class Medicines
{
    public string Title;
    public string IndicationsForUse;
    public string Manufacturer;
    public bool CanBeSoldWithoutPrescription;


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