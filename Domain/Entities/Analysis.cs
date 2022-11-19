
namespace Domain.Entities;

public class Analysis
{
    public string Title;
    public int TimeForPrepearing;


    public Analysis(string title, int timeForPrepearing)
    {
        Title = title;
        TimeForPrepearing = timeForPrepearing;
    }

}