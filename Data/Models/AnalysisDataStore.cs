using Domain.Entities;

namespace Data.Models;

public class AnalysisDataStore
{
    public string Title;
    public int TimeForPrepearing;
    public int Id;
    
    public AnalysisDataStore(Analysis analysis, int id)
    {
        Title = new string(analysis.Title);
        TimeForPrepearing = analysis.TimeForPrepearing;
        Id = id;
    }
    
    
    public AnalysisDataStore(string title, int timeForPrepearing, int id)
    {
        Title = title;
        TimeForPrepearing = timeForPrepearing;
        Id = id;
    }


    public AnalysisDataStore()
    {
        Title = new string("title");
        TimeForPrepearing = Id = 0;
    }

    public Analysis MapToAnalyzis()
    {
        return new Analysis(Title, TimeForPrepearing);
    }

}