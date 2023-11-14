namespace Domain.Entities.Polyclinic.Analysis;

public class Analysis
{
    public string Type { get; }

    public Analysis(string type, string description, string peculiarities, TimeSpan timeToTake, TimeSpan timeToPrepear, uint iD)
    {
        Type = type;
        Description = description;
        Peculiarities = peculiarities;
        TimeToTake = timeToTake;
        TimeToPrepear = timeToPrepear;
        ID = iD;
    }

    public string Description { get; }
    public string Peculiarities { get; }

    public TimeSpan TimeToTake { get; }
    public TimeSpan TimeToPrepear { get; }

    public uint ID { get; }
}