using System.Reactive.Linq;
using System.Reactive.Subjects;

using System.Text.Json;


namespace Data.Repositories;


abstract public class BaseRepository<T>
{
<<<<<<< HEAD
    private XmlSerializer _serializer;
    private XmlWriter _writer;
    private XmlReader _reader;
    private FileStream _fs;
=======
    private Stream _fs;
>>>>>>> temporary

    private string _path;
    public IObservable<List<T>> AsObservable { get; }
    private BehaviorSubject <List<T>> _subject { get; }
<<<<<<< HEAD
    protected  BaseRepository(string path)
=======

 
    
    protected BaseRepository(string path)
>>>>>>> temporary
    {
        _path = path;
        _subject = new BehaviorSubject<List<T>>(new List<T>());
        AsObservable = _subject.AsObservable();
    }
    
    abstract protected bool CompareEntities(T changedEntity, T entity);
    protected void Change(T changedEntity)
    {
<<<<<<< HEAD
        PrepearTools();
=======
>>>>>>> temporary
        
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(changedEntity, entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationJson(newEntities);
            break;
        }
        _fs.Close();
    }
    
    protected void Remove(T delitingEmtity)
    {
<<<<<<< HEAD
        PrepearTools();
        
=======
        _fs = GetStream();
>>>>>>> temporary
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
    
<<<<<<< HEAD
    protected void SerializationXml(List<T> entities)
    {
        PrepearTools();
        
        _subject.OnNext(entities);
        _serializer.Serialize(_writer, entities);
        _fs.Close();
=======
    protected void SerializationJson(List<T> entities)
    {
        _fs = GetStream();
        _subject.OnNext(entities);
        JsonSerializer.Serialize(GetStream(), entities);
        _fs.Close();

>>>>>>> temporary
    }
    
    protected void Append(T entity)
    {
        PrepearTools();
        
        var newEntities = new List<T>(_subject.Value);
        newEntities.Add(entity);
<<<<<<< HEAD
        SerializationXml(newEntities);
        _fs.Close();
    }

    protected  List<T> DeserializationXml()
    {
        PrepearTools();
        
        var deserialized = new List<T>();
        object? temp = (_serializer.Deserialize(_fs));
        if (temp != null)
        {
            deserialized = (List<T>)temp;
            _subject.OnNext(deserialized);
        }
        _fs.Close();

        return deserialized;
    }
    
    private void PrepearTools()
    {
        _fs = File.Open(_path, FileMode.Open);
        _fs.Seek(0, SeekOrigin.Begin);
        _serializer = new(typeof(List<T>));
        _writer = XmlWriter.Create(_fs, new XmlWriterSettings()
        {
            Indent = true, 
            IndentChars = "    ",
            
        });
        _reader = XmlReader.Create(_fs, new XmlReaderSettings());
        _fs.Seek(0, SeekOrigin.Begin);

=======
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
>>>>>>> temporary
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