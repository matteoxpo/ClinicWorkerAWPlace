namespace Domain.Entities.People.Attribute;

public sealed class Benefit
{
    public Benefit(string type, string description, double discount, int retirementAge, int id)
    {
        Type = type;
        Description = description;
        Discount = discount;
        RetirementAge = retirementAge;
        ID = id;
    }
    public string Type { get; }
    public string Description { get; }
    public double Discount { get; }
    public int RetirementAge { get; }
    public int ID { get; }
}