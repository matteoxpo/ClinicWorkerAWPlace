using System.Text.Json.Serialization;

namespace Domain.Entities.People;

[Serializable]
public class Client
{
    [JsonIgnore] public DateTime MeetTime;

    public Client()
    {
        Complaints = new string("complaints");
        Name = new string("name");
        Surname = new string("surname");
        Analyzes = new List<ReferenceForAnalysis>();
        Appointments = new List<Appointment>();
        Id = new string("0");
        MeetTime = new DateTime(0);
    }

    public Client(string name, string surname, DateTime birthTime, IEnumerable<ReferenceForAnalysis> analyses,
        List<Appointment> appointments, string id, string complaints, DateTime meetTime)
    {
        MeetTime = meetTime;
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Appointments = new List<Appointment>(appointments);
        Id = id;
        Complaints = complaints;
    }

    public Client(string name, string surname, DateTime birthTime, IEnumerable<ReferenceForAnalysis> analyses,
        string id)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Appointments = new List<Appointment>();
        Complaints = new string("complaints");
        MeetTime = new DateTime(0);
        Id = id;
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    [JsonIgnore] public string Complaints { get; set; }
    [JsonIgnore] public IEnumerable<ReferenceForAnalysis> Analyzes { get; set; }
    public string Id { get; set; }

    [field: NonSerialized] [JsonIgnore] public IEnumerable<Appointment> Appointments { get; set; }

    public override string ToString()
    {
        return string.Join(" ",
            Name,
            Surname,
            MeetTime.Equals(new DateTime(0))
                ? "\nДата рождения: " + DateOfBirth.ToString("MM/dd/yyyy")
                : "\nВремя записи: " + MeetTime);
    }
}