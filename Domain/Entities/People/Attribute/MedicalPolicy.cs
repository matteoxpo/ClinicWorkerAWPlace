
namespace Domain.Entities.People.Attribute;

public sealed class MedicalPolicy
{
    public string Serial { get; }

    public MedicalPolicy(string serial, string number, int id)
    {
        Serial = serial;
        Number = number;
        ID = id;
    }

    public string Number { get; }
    public int ID { get; }
}