using Domain.Entities;
using Domain.Repositories;

namespace Data.Repositories;

public class AnalysisRepository : BaseRepository<Analysis>,IAnalysisRepository
{
    private static AnalysisRepository? _globalRepositoryInstance;

    public static AnalysisRepository GetInstance()
    { 
        return _globalRepositoryInstance ??= new AnalysisRepository(
            "../../../../Data/DataSets/Analysis.json");
    }
    public void Update(Analysis nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(Analysis oldEntity)
    {
        Remove(oldEntity);
    }

    public void Add(Analysis newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<Analysis> Read()
    {
        return DeserializationJson();
    }

    public override bool CompareEntities(Analysis entity1, Analysis entity2)
    {
        return entity1.Title.Equals(entity2.Title) && entity1.TimeForPrepearing.Equals(entity2.TimeForPrepearing);
    }

    public AnalysisRepository(string path) : base(path) { }
}