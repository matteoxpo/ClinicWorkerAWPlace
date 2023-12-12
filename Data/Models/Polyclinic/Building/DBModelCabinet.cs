namespace Data.Models.Polyclinic.Building;

public sealed class DBModelCabinet
{
    public DBModelCabinet(string number, string description, uint id)
    {
        Number = number ?? throw new NullReferenceException("Number is null");
        Description = description ?? throw new NullReferenceException("Description is null");
        ID = id;
    }

    public string Number { get; set; }

    public string Description { get; set; }
    public uint ID { get; }
}