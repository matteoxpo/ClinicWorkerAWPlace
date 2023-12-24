namespace Domain.Entities.People.Attribute;

public sealed class Education
{
    public string Serial { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public int HumanUserId { get; set; }
    public int ID { get; }
    public DateTime Date { get; set; }
    public Education(string number, string serial, DateTime date, string description, int humanUserId, int id)
    {
        Serial = serial;
        Number = number;
        Description = description;
        Date = date;
        HumanUserId = humanUserId;
        ID = id;
    }
}