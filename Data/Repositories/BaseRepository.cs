using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Domain.Repositories;


namespace Data.Repositories;


abstract public class BaseRepository<T>
{
    private Stream? _fs;

    private string _path;
    protected IObservable<List<T>> AsObservable { get; }
    private BehaviorSubject <List<T>> _entitiesSubject { get; }

 
    
    protected BaseRepository(string path)
    {
        _path = path;
        _entitiesSubject = new BehaviorSubject<List<T>>(new List<T>());
        AsObservable = _entitiesSubject.AsObservable();
    }
    
    public abstract bool CompareEntities(T changedEntity, T entity);
    protected void Change(T changedEntity)
    {
        var newEntities = new List<T>(_entitiesSubject.Value);
        foreach (var entity in _entitiesSubject.Value)
        {
            if (!CompareEntities(changedEntity, entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationJson(newEntities);
            break;
        }
    }
    
    protected void Remove(T delitingEmtity)
    {
        var newEntities = new List<T>(_entitiesSubject.Value);
        foreach (var entity in _entitiesSubject.Value)
        {
            if (!CompareEntities(delitingEmtity,entity)) continue;
            newEntities.Remove(entity);
            SerializationJson(newEntities);
            break;
        }
    }
    
    protected void SerializationJson(List<T> entities)
    {
        if (entities is null) return;
        
        _entitiesSubject.OnNext(entities);
        File.WriteAllText(_path,"");
        _fs = GetStream();
        JsonSerializer.SerializeAsync(_fs, entities, _options);
        _fs.Close();
    }
    
    protected void Append(T entity)
    {
        var newEntities = new List<T>(_entitiesSubject.Value);
        newEntities.Add(entity);
        SerializationJson(newEntities);
    }

    private JsonSerializerOptions _options = new ()
    {
        WriteIndented = true,
    };

    protected  List<T> DeserializationJson()
    {
        List<T> deserialized = null;
        _fs = GetStream();
        try
        {
            _fs = GetStream();
            deserialized = JsonSerializer.Deserialize<List<T>>(_fs, _options);
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            _fs.Close();
            if (deserialized is null)
            {
                deserialized = new List<T>();
            }
        }
        _entitiesSubject.OnNext(deserialized);
        return deserialized;

    }
    
    private Stream GetStream()=> new FileStream
        (
            _path,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            4096,
            FileOptions.None
        );
   


}