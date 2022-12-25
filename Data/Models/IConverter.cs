namespace Data.Models;

public interface IConverter<TEntity, TStorageEntity> where TStorageEntity : new()
{
    public TEntity ConvertToEntity(TStorageEntity entity);
    public TStorageEntity ConvertToStorageEntity(TEntity entity);

    public IEnumerable<TStorageEntity> ConvertToStorageEntity(IEnumerable<TEntity> entities)
    {
        return entities.Select(ConvertToStorageEntity);
    }

    public IEnumerable<TEntity> ConvertToEntity(IEnumerable<TStorageEntity> entities)
    {
        return entities.Select(ConvertToEntity);
    }
}