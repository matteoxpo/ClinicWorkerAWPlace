using System.Xml;
using System.Xml.Serialization;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Data.Repositories;


abstract public class BaseRepository<T>
{
    private XmlSerializer _serializer;
    private XmlWriter _writer;
    private XmlReader _reader;
    private FileStream _fs;

    private string _path;
    public IObservable<List<T>> AsObservable { get; }
    private BehaviorSubject <List<T>> _subject { get; }
    protected  BaseRepository(string path)
    {
        _path = path;
        _subject = new BehaviorSubject<List<T>>(new List<T>());
        AsObservable = _subject.AsObservable();
    }
    
    abstract protected bool CompareEntities(T changedEntity, T entity);
    protected void Change(T changedEntity)
    {
        PrepearTools();
        
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(changedEntity, entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationXml(newEntities);
            break;
        }
        _fs.Close();
    }
    
    protected void Remove(T delitingEmtity)
    {
        PrepearTools();
        
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(delitingEmtity,entity)) continue;
            newEntities.Remove(entity);
            SerializationXml(newEntities);
            break;
        }
        _fs.Close();
    }
    
    protected void SerializationXml(List<T> entities)
    {
        PrepearTools();
        
        _subject.OnNext(entities);
        _serializer.Serialize(_writer, entities);
        _fs.Close();
    }
    
    protected void Append(T entity)
    {
        PrepearTools();
        
        var newEntities = new List<T>(_subject.Value);
        newEntities.Add(entity);
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

    }


}