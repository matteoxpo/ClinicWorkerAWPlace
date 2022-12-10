namespace Domain.Entities;

[Serializable]
public class ReferenceForAnalysis
{
    public Analysis Analysis { get; }
    public DateTime AnalysisTime { get; }
    public ReferenceForAnalysis(Analysis analysis, DateTime analysisTime)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
    }
    
    public ReferenceForAnalysis()
    {
        Analysis = new Analysis();
        AnalysisTime = new DateTime(0);
    }

}