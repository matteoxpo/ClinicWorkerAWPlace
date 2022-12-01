using System.Reactive.Linq;
using System.Reactive.Subjects;

using System.Text.Json;


namespace Data.Repositories;


abstract public class BaseRepository<T>
{
    private Stream _fs;

    private string _path;
    public IObservable<List<T>> AsObservable { get; }
    private BehaviorSubject <List<T>> _subject { get; }

 
    
    protected BaseRepository(string path)
    {
        _path = path;
        _subject = new BehaviorSubject<List<T>>(new List<T>());
        AsObservable = _subject.AsObservable();
    }
    
    abstract protected bool CompareEntities(T changedEntity, T entity);
    protected void Change(T changedEntity)
    {
        
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
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
        _fs = GetStream();
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(delitingEmtity,entity)) continue;
            newEntities.Remove(entity);
            SerializationJson(newEntities);
            break;
        }
        _fs.Close();
    }
    
    
    // make protected
    protected void SerializationJson(List<T> entities)
    {
        _fs = GetStream();
        _subject.OnNext(entities);
        JsonSerializer.Serialize(GetStream(), entities, _options);
        _fs.Close();

    }
    
    protected void Append(T entity)
    {
        var newEntities = new List<T>(_subject.Value);
        newEntities.Add(entity);
        SerializationJson(newEntities);
    }

    private JsonSerializerOptions _options = new JsonSerializerOptions()
    {
        WriteIndented = true,
    };

    protected  List<T> DeserializationJson()
    {
        _fs = GetStream();
        var deserialized = JsonSerializer.Deserialize<List<T>>(_fs, _options);
        _fs.Close();
        return deserialized;
    }
    
    private Stream GetStream()=> _fs = new FileStream
        (
            _path,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            4096,
            FileOptions.None
        );
   


}