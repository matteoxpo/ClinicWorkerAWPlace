namespace Domain.Entities.Roles.Doctor;

public class Analysis
{
    public Analysis(string title, TimeSpan timeForPrepearing, TimeSpan timeForTaking, string id)
    {
        Title = new string(title);
        TimeForTaking = timeForTaking;
        TimeForPrepearing = timeForPrepearing;
        Id = new string(id);
    }

    public string Title { get; }
    public TimeSpan TimeForPrepearing { get; }
    public TimeSpan TimeForTaking { get; }
    public string Id { get; }

    public override string ToString()
    {
        return new string(Title) +
               "\nВремя взятия анализа: " +
               (TimeForTaking.Minutes > 60 ? TimeForTaking.Hours.ToString() : TimeForTaking.Minutes.ToString()) +
               "\nВремя подготовки: " +
               (TimeForPrepearing.Minutes > 60 ? TimeForPrepearing.Hours.ToString() : TimeForTaking.Minutes.ToString());
    }
}