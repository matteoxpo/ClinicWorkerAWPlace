using Domain.Entities;
using Domain.Repositories;

namespace Data.Repositories;

public class MedicinesRepository : BaseRepository<Medicines>, IMedicinesRepository
{
    public MedicinesRepository(string path) : base(path) { }
    
    private static MedicinesRepository? _globalRepositoryInstance;

    public static MedicinesRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new MedicinesRepository(
            "../../../../Data/DataSets/Medicines.json");
    }

    public void Update(Medicines nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(Medicines oldEntity)
    {
        Remove(oldEntity);
    }

    public void Add(Medicines newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<Medicines> Read()
    {
        return DeserializationJson();
    }

    public override bool CompareEntities(Medicines entity1, Medicines entity2)
    {
        return entity1.Title.Equals(entity2.Title) && entity1.Manufacturer.Equals(entity2.Manufacturer);
    }
}