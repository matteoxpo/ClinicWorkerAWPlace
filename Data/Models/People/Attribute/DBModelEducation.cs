namespace Data.Models.People.Attribute;

public sealed class DBModelEducation
{
    public string Serial { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }

    public uint ID { get; }

    public DBModelEducation(string number, string serial, DateTime graduationDate, string description, uint id)
    {
        Description = description;
        Number = number;
        Serial = serial;
        GraduationDate = graduationDate;
        ID = id;
    }

    public DateTime GraduationDate { get; set; }
}