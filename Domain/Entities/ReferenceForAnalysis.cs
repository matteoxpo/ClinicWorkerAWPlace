namespace Domain.Entities;

public class ReferenceForAnalysis
{
    public ReferenceForAnalysis(Analysis analysis, DateTime analysisTime, string clientId, string? result = null)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
        ClientId = clientId;
        Result = result ?? new string("Результата анализа ещё нет");
    }
 
    

    public Analysis Analysis { get; }
    public DateTime AnalysisTime { get; }
    public string? Result { get; }
    public string ClientId { get; }

    public override string ToString()
    {
        return "Анализ: " + Analysis + "\nНаправление на дату:" + AnalysisTime;
    }
}