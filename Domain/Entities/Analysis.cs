namespace Domain.Entities;

[Serializable]
public class Analysis
{
    public Analysis(string title, TimeSpan timeForPrepearing, TimeSpan timeForTaking, string id)
    {
        Title = title;
        TimeForTaking = timeForTaking;
        TimeForPrepearing = timeForPrepearing;
        Id = id;
    }

    public Analysis()
    {
        Id = new string("0");
        Title = new string("Title");
        TimeForPrepearing = new TimeSpan(0);
    }

    public string Title { get; set; }
    public TimeSpan TimeForPrepearing { get; set; }
    public TimeSpan TimeForTaking { get; set; }
    public string Id { get; set; }

    public override string ToString()
    {
        return new string(Title) +
               "\nВремя взятия анализа: " +
               (TimeForTaking.Minutes > 60 ? TimeForTaking.Hours.ToString() : TimeForTaking.Minutes.ToString()) +
               "\nВремя подготовки: " +
               (TimeForPrepearing.Minutes > 60 ? TimeForPrepearing.Hours.ToString() : TimeForTaking.Minutes.ToString());
    }
}