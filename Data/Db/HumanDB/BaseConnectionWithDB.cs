using Data.Models;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Data.Exeptions;
using Domain.Entities.People;

namespace Data.Db.HumanDB;


// abstracr soon
public class BaseConnectionWithDB<T>
{
    protected XmlSerializer _serializer;
    protected XmlWriter _writer;
    protected XmlReader _reader;
    protected FileStream _fs;
    
    
    public void Update(T changedEntity, Func<T, bool> predicate)
    {
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!predicate(entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationXml(newEntities);
            break;
        }
    }
    
    public void Delete(T delitingEmtity, Func<T, bool> predicate)
    {
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!predicate(entity)) continue;
            newEntities.Remove(entity);
            SerializationXml(newEntities);
            break;
        }
    }
    
    public IEnumerable<T> Read() => DeserializationXml();
    
    public void Add(T entity)
    {
        var newEntities = new List<T>(_subject.Value);
        newEntities.Add(entity);
        SerializationXml(newEntities);
    }
    public IObservable<IEnumerable<T>> AsObservable { get; }
    private BehaviorSubject <IEnumerable<T>> _subject { get; }
    public BaseConnectionWithDB(string path)
    {
        _fs = File.Open(path, FileMode.Open);
        _serializer = new(typeof(List<T>));
        _writer = XmlWriter.Create(_fs, new XmlWriterSettings() { Indent = true, IndentChars = "    ", });
        _reader = XmlReader.Create(_fs, new XmlReaderSettings());
        _subject = new BehaviorSubject<IEnumerable<T>>(new List<T>());
        AsObservable = _subject.AsObservable();
    }

    public IEnumerable<T> DeserializationXml()
    {
        _fs.Seek(0, SeekOrigin.Begin);
        List<T> deserialized = new();
        object? temp = (_serializer.Deserialize(_fs));
        if (temp != null)
        {
            deserialized = (List<T>)temp;
            _subject.OnNext(deserialized);
        }
        return deserialized;
    }

    public void SerializationXml(IEnumerable<T> entities)
    {
        _fs.Seek(0, SeekOrigin.Begin);
        _subject.OnNext(entities);
        _serializer.Serialize(_writer, entities);
    }
}