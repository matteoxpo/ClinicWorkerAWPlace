namespace Domain.Entities;

[Serializable]
public class RefForAnalysis
{
    public Analysis Analysis { get; }
    public DateTime AnalysisTime { get; }
    public RefForAnalysis(Analysis analysis, DateTime analysisTime)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
    }
    
    public RefForAnalysis()
    {
        Analysis = new Analysis();
        AnalysisTime = new DateTime(0);
    }

}