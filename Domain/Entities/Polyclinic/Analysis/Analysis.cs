namespace Domain.Entities.Polyclinic.Analysis;

public sealed class Analysis
{
    public string Type { get; }

    public Analysis(string type, string description, string peculiarities, int id)
    {
        Type = type;
        Description = description;
        Peculiarities = peculiarities;
        ID = id;
    }

    public string Description { get; }
    public string Peculiarities { get; }


    public int ID { get; }
}