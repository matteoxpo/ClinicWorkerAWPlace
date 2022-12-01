using Domain.Entities;

namespace Data.Models;

[Serializable]
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
        TimeForPrepearing = 0;
        Id = 0;
    }

    public Analysis MapToAnalyzis()
    {
        return new Analysis(Title, TimeForPrepearing);
    }
    
    public List<Analysis> MapToAnalyzis(List<AnalysisDataStore> analysisDataStores)
    {
        List<Analysis> analys = new List<Analysis>();
        foreach (var analysis in analysisDataStores)
        {
            analys.Add(analysis.MapToAnalyzis());
        }
        return analys;
    }

}