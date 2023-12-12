namespace Data.Models.Polyclinic.Analysis;

public sealed class DBModelAnalysis
{
    public string Type { get; }

    public DBModelAnalysis(string type, string description, string peculiarities, TimeSpan timeToTake, TimeSpan timeToPrepeare, int iD)
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