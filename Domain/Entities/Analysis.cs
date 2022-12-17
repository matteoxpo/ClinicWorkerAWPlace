namespace Domain.Entities;

[Serializable]
public class Analysis
{
    public string Title { get; }
    public int TimeForPrepearing;
    public string Id;


    public Analysis(string title, int timeForPrepearing, string id)
    {
        Title = title;
        TimeForPrepearing = timeForPrepearing;
        Id = id;
    }
    
    public Analysis()
    {
        Id = new string("0");
        Title = new string("Title");
        TimeForPrepearing = 0;
    }
    
    public override string ToString() =>  new (Title);

}