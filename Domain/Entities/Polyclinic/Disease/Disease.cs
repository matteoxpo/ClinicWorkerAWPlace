namespace Domain.Entities.Polyclinic.Disease;


public sealed class Disease
{
    public string Name { get; }

    public string Description { get; }

    public Disease(string name, string description, Transmission transmission, uint iD)
    {
        Name = name;
        Description = description;
        ID = iD;
        Transmission = transmission;
    }

    public Transmission Transmission { get; }

    public uint ID { get; }
}