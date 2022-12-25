namespace Domain.Entities;

[Serializable]
public class ReferenceForAnalysis
{
    public ReferenceForAnalysis(Analysis analysis, DateTime analysisTime, string clientId)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
        ClientId = clientId;
    }

    public ReferenceForAnalysis()
    {
        Analysis = new Analysis();
        AnalysisTime = new DateTime(0);
        ClientId = new string("0");
    }

    public Analysis Analysis { get; }
    public DateTime AnalysisTime { get; }
    public string ClientId { get; }

    public override string ToString()
    {
        return "Анализ: " + Analysis + "\nНаправление на дату:" + AnalysisTime;
    }
}