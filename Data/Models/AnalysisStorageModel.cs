using Domain.Entities;

namespace Data.Models;

[Serializable]
public class AnalysisStorageModel : IConverter<Analysis, AnalysisStorageModel>
{
    public AnalysisStorageModel(string title, TimeSpan timeForPrepearing, TimeSpan timeForTaking ,string id)
    {
        Title = new string(title);
        TimeForTaking = timeForTaking;
        TimeForPrepearing = timeForPrepearing;
        Id = new string(id);
    }

    public AnalysisStorageModel()
    {
        Id = new string("0");
        Title = new string("Title");
        TimeForPrepearing = new TimeSpan(0);
    }

    public string Title { get; set; }
    public TimeSpan TimeForPrepearing { get; set; }
    public TimeSpan TimeForTaking { get; set; }
    public string Id { get; set; }

    public Analysis ConvertToEntity(AnalysisStorageModel entity)
    {
        return new Analysis(entity.Title, entity.TimeForPrepearing, entity.TimeForTaking, entity.Id);
    }

    public AnalysisStorageModel ConvertToStorageEntity(Analysis entity)
    {
        return new AnalysisStorageModel(entity.Title, entity.TimeForPrepearing, entity.TimeForTaking, entity.Id);
    }

    public override string ToString()
    {
        return new string(Title) +
               "\nВремя взятия анализа: " +
               (TimeForTaking.Minutes > 60 ? TimeForTaking.Hours.ToString() : TimeForTaking.Minutes.ToString()) +
               "\nВремя подготовки: " +
               (TimeForPrepearing.Minutes > 60 ? TimeForPrepearing.Hours.ToString() : TimeForTaking.Minutes.ToString());
    }
}