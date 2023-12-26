namespace Domain.Entities.Polyclinic.Analysis;

public sealed class Analysis
{
    public string Type { get; }

    public Analysis(string type, string description, int id)
    {
        Type = type;
        Description = description;
        ID = id;
    }

    public string Description { get; }


    public int ID { get; }
}