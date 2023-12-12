namespace Domain.Entities.Polyclinic.Analysis;

public sealed class Analysis
{
    public string Type { get; }

    public Analysis(string type, string description, string peculiarities, TimeSpan timeToTake, TimeSpan timeToPrepeare, int iD)
    {
        Type = type;
        Description = description;
        Peculiarities = peculiarities;
        TimeToTake = timeToTake;
        TimeToPrepeare = timeToPrepeare;
        ID = iD;
    }

    public string Description { get; }
    public string Peculiarities { get; }

    public TimeSpan TimeToTake { get; }
    public TimeSpan TimeToPrepeare { get; }

    public int ID { get; }
}