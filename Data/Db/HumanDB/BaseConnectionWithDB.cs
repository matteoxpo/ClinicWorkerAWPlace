using Data.Models;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Data.Exeptions;
using Domain.Entities.People;

namespace Data.Db.HumanDB;


// abstracr soon
public class BaseConnectionWithDB<T>
{

    protected List<T> _entity;

    protected XmlSerializer serializer;
    protected XmlWriter writer;
    protected XmlReader reader;
    protected FileStream fs;


    public BaseConnectionWithDB(string path)
    {
        fs = File.Open(path, FileMode.Open);
        serializer = new(typeof(List<EmployeeDataStore>));
        writer = XmlWriter.Create(fs, new XmlWriterSettings() { Indent = true, IndentChars = "    ", });
        reader = XmlReader.Create(fs, new XmlReaderSettings());

        // _entity = new List<Employee>(DeserializationXml());
        _entity = DeserializationXml();
    }

    public List<T> DeserializationXml()
    {
        fs.Seek(0, SeekOrigin.Begin);
        List<T> deserialized = new();
        object? temp = (serializer.Deserialize(fs));
        if (temp != null)
            deserialized = (List<T>)temp;
        // else 
        // throw new ExecutionDe            deserialize exeption

        return deserialized;
    }

    public void SerializationXml()
    {
        fs.Seek(0, SeekOrigin.Begin);
        serializer.Serialize(writer, _entity);
    }

    public bool IsFileOpened
    {
        get
        {
            return fs != null;
        }
    }

}