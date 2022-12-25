using Data.Models;

namespace Domain.Entities;

[Serializable]
public class ReferenceForAnalysisStorageModel : IConverter<ReferenceForAnalysis, ReferenceForAnalysisStorageModel>
{
    public ReferenceForAnalysisStorageModel(AnalysisStorageModel analysis, DateTime analysisTime, string clientId)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
        ClientId = clientId;
    }

    public ReferenceForAnalysisStorageModel()
    {
        Analysis = new AnalysisStorageModel();
        AnalysisTime = new DateTime(0);
        ClientId = new string("0");
    }

    public AnalysisStorageModel Analysis { get; set; }
    public DateTime AnalysisTime { get; set; }
    public string ClientId { get; set; }


    public ReferenceForAnalysis ConvertToEntity(ReferenceForAnalysisStorageModel entity)
    {
        return new ReferenceForAnalysis(entity.Analysis.ConvertToEntity(entity.Analysis), entity.AnalysisTime,
            entity.ClientId);
    }

    public ReferenceForAnalysisStorageModel ConvertToStorageEntity(ReferenceForAnalysis entity)
    {
        return new ReferenceForAnalysisStorageModel(
            new AnalysisStorageModel(entity.Analysis.Title, entity.Analysis.TimeForPrepearing,
                entity.Analysis.TimeForTaking, entity.Analysis.Id),
            entity.AnalysisTime, entity.ClientId);
    }

    public override string ToString()
    {
        return "Анализ: " + Analysis + "\nНаправление на дату:" + AnalysisTime;
    }
}