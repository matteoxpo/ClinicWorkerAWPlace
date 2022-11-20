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

namespace Data.Repositories;


// abstracr soon
abstract public class BaseRepository<T>
{
    private XmlSerializer _serializer;
    private XmlWriter _writer;
    private XmlReader _reader;
    private FileStream _fs;

    abstract protected bool CompareEntities(T changedEntity, T entity);
    
    
    protected void Change(T changedEntity)
    {
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(changedEntity, entity)) continue;
            newEntities.Remove(entity);
            newEntities.Add(changedEntity);
            SerializationXml(newEntities);
            break;
        }
    }
    
    protected void Remove(T delitingEmtity)
    {
        var newEntities = new List<T>(_subject.Value);
        foreach (var entity in _subject.Value)
        {
            if (!CompareEntities(delitingEmtity,entity)) continue;
            newEntities.Remove(entity);
            SerializationXml(newEntities);
            break;
        }
    }
    
    // read
    protected void SerializationXml(IEnumerable<T> entities)
    {
        _fs.Seek(0, SeekOrigin.Begin);
        _subject.OnNext(entities);
        _serializer.Serialize(_writer, entities);
    }
    
    protected void Append(T entity)
    {
        var newEntities = new List<T>(_subject.Value);
        newEntities.Add(entity);
        SerializationXml(newEntities);
    }
    public IObservable<IEnumerable<T>> AsObservable { get; }
    private BehaviorSubject <IEnumerable<T>> _subject { get; }
    protected  BaseRepository(string path)
    {
        _fs = File.Open(path, FileMode.Open);
        _serializer = new(typeof(List<T>));
        _writer = XmlWriter.Create(_fs, new XmlWriterSettings() { Indent = true, IndentChars = "    ", });
        _reader = XmlReader.Create(_fs, new XmlReaderSettings());
        _subject = new BehaviorSubject<IEnumerable<T>>(new List<T>());
        AsObservable = _subject.AsObservable();
    }

    protected  IEnumerable<T> DeserializationXml()
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

 
}