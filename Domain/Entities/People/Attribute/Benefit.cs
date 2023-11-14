namespace Domain.Entities.People.Attribute;

public class Benefit
{


    public Benefit(string type, string description, double discount, int retirementAge)
    {
        Type = type;
        Description = description;
        Discount = discount;
        RetirementAge = retirementAge;
    }

    public string Type { get; }
    public string Description { get; }
    public double Discount { get; }
    public int RetirementAge { get; }
}