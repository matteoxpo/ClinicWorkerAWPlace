using Domain.Entities.Polyclinic.Disease;

namespace Data.Models.Polyclinic.Disease;
public sealed class DBModelDisease
{
    public string Name { get; }

    public string Description { get; }

    public DBModelDisease(string name, string description, Transmission transmission, uint iD)
    {
        Name = name;
        Description = description;
        ID = iD;
        Transmission = transmission;
    }

    public Transmission Transmission { get; }

    public uint ID { get; }
}