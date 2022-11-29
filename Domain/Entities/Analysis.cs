
namespace Domain.Entities;

[Serializable]
public class Analysis
{
    public string Title;
    public int TimeForPrepearing;


    public Analysis(string title, int timeForPrepearing)
    {
        Title = title;
        TimeForPrepearing = timeForPrepearing;
    }
    
    public Analysis()
    {
        Title = new string("Title");
        TimeForPrepearing = 0;
    }

}