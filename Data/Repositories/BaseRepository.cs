using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Data.Models;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, TStorageEntity>
    where TStorageEntity : IConverter<TEntity, TStorageEntity>, new()
{
    private static TStorageEntity _storageEntity = new();

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    private readonly string _path;
    private Stream? _fs;


    protected BaseRepository(string path)
    {
        _path = path;
        EntitiesSubject = new BehaviorSubject<List<TEntity>>(new List<TEntity>());
        AsObservable = EntitiesSubject.AsObservable();
    }

    protected IObservable<List<TEntity>> AsObservable { get; }
    private BehaviorSubject<List<TEntity>> EntitiesSubject { get; }


    public abstract bool CompareEntities(TEntity entity1, TEntity entity2);

    protected void Change(TEntity changedEntity)
    {
        var newEntities = new List<TEntity>(EntitiesSubject.Value);
        foreach (var entity in EntitiesSubject.Value)
        {
            if (!CompareEntities(changedEntity, entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationJson(newEntities);
            break;
        }
    }

    protected void Remove(TEntity delitingEmtity)
    {
        var newEntities = new List<TEntity>(EntitiesSubject.Value);
        foreach (var entity in EntitiesSubject.Value.Where(entity => CompareEntities(delitingEmtity, entity)))
        {
            newEntities.Remove(entity);
            EntitiesSubject.OnNext(newEntities);
            SerializationJson(newEntities);
            break;
        }
    }

    private void SerializationJson(List<TEntity>? entities)
    {
        if (entities is null) return;

        _fs = GetStream();
        JsonSerializer.Serialize(_fs, _storageEntity.ConvertToStorageEntity(entities), _options);
        _fs.Close();
        EntitiesSubject.OnNext(entities);
    }

    protected void Append(TEntity entity)
    {
        var newEntities = new List<TEntity>(EntitiesSubject.Value);
        newEntities.Add(entity);
        SerializationJson(newEntities);
    }

    protected IEnumerable<TEntity> DeserializationJson()
    {
        if (EntitiesSubject.Value.Count > 0) return EntitiesSubject.Value;

        List<TStorageEntity>? deserialized = null;
        try
        {
            _fs = GetStream();
            deserialized = JsonSerializer.Deserialize<List<TStorageEntity>>(_fs, _options);
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            _fs?.Close();
            deserialized ??= new List<TStorageEntity>();
        }

        EntitiesSubject.OnNext(new List<TEntity>(_storageEntity.ConvertToEntity(deserialized)));
        return _storageEntity.ConvertToEntity(deserialized);
    }

    private Stream GetStream()
    {
        return new FileStream
        (
            _path,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            4096,
            FileOptions.None
        );
    }
}