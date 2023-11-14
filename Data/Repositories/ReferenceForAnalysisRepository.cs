using Domain.Entities;
using Domain.Entities.Role.Doctor;
using Domain.Repositories;

namespace Data.Repositories;

public class ReferenceForAnalysisRepository : BaseRepository<ReferenceForAnalysis, ReferenceForAnalysisStorageModel>,
    IReferenceForAnalysisRepository
{
    private static ReferenceForAnalysisRepository? _globalRepositoryInstance;

    public ReferenceForAnalysisRepository(string path) : base(path)
    {
    }

    public void Update(ReferenceForAnalysis nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(ReferenceForAnalysis oldEntity)
    {
        Remove(oldEntity);
    }

    public void Add(ReferenceForAnalysis newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<ReferenceForAnalysis> Read()
    {
        return DeserializationJson();
    }

    public override bool CompareEntities(ReferenceForAnalysis entity1, ReferenceForAnalysis entity2)
    {
        return entity1.AnalysisTime.Equals(entity2.AnalysisTime) &&
               entity1.ClientId.Equals(entity2.ClientId) &&
               entity1.Analysis.Id.Equals(entity2.Analysis.Id);
    }

    public static ReferenceForAnalysisRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new ReferenceForAnalysisRepository(
            "../../../../Data/DataSets/ReferenceForAnalysis.json");
    }
}