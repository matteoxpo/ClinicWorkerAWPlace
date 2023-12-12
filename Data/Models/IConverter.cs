namespace Data.Models;

public interface IDBConverter<TEntity, TStorageEntity> where TStorageEntity : new()
{
    TEntity ConvertFromStorageEntity(TStorageEntity entity);
    ICollection<TEntity> ConvertFromStorageEntity(IEnumerable<TStorageEntity> entities)
    {
        var TEntities = new TEntity[entities.Count()];
        foreach (var entity in entities)
        {
            TEntities.Append(ConvertFromStorageEntity(entity));
        }
        return TEntities;
    }

    TEntity ConvertFromEntity();
    TStorageEntity ConvertFromEntity(TEntity entity);
    ICollection<TStorageEntity> ConvertFromEntity(IEnumerable<TEntity> entities)
    {
        var TStorageEntities = new TStorageEntity[entities.Count()];
        foreach (var entity in entities)
        {
            TStorageEntities.Append(ConvertFromEntity(entity));
        }
        return TStorageEntities;
    }
}