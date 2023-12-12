
namespace Data.Models.People.Attribute;

public sealed class DBModelMedicalPolicy
{
    public string Serial { get; }

    public DBModelMedicalPolicy(string serial, string number, uint id)
    {
        Serial = serial;
        Number = number;
        ID = id;
    }

    public string Number { get; }
    public uint ID { get; }
}