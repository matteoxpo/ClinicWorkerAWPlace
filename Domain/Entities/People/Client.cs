using System.Text.Json.Serialization;

namespace Domain.Entities.People;

[Serializable]
public class Client
{
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
        IEnumerable<Appointment> appointments, string id, string complaints, DateTime meetTime)
    {
        MeetTime = meetTime;
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Appointments = new List<Appointment>(appointments);
        Id = id;
        Complaints = new string(complaints);
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
        Id = new string(id);
    }

    public string Name { get; }
    public string Surname { get; }
    public DateTime DateOfBirth { get; }
    public string Id { get; }

    [JsonIgnore] public DateTime MeetTime { get; }
    [JsonIgnore] public string Complaints { get; }
    [JsonIgnore] public IEnumerable<ReferenceForAnalysis> Analyzes { get; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; }

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