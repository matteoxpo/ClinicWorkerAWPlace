namespace Domain.Entities.People;

[Serializable]
public class Client
{
    public string Name { get; set; }
    public string Surname { get ; set; }
    public  DateTime DateOfBirth { get; set; }
    [field: NonSerialized] public DateTime MeetTime;
    [field: NonSerialized] public string Complaints { get; set; }
    public IEnumerable<ReferenceForAnalysis> Analyzes { get; set; }
    public string Id { get; set;}
    
    [field: NonSerialized] public IEnumerable<Appointment> Appointments { get; set; }

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

    public Client(string name, string surname, DateTime birthTime, IEnumerable<ReferenceForAnalysis> analyses, List<Appointment> appointments, string id, string complaints, DateTime meetTime)
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
    public Client(string name, string surname, DateTime birthTime, IEnumerable<ReferenceForAnalysis> analyses, string id) 
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

    public override string ToString() => string.Join(" ", 
        Name,
        Surname,
        "\nДата рождения: " + DateOfBirth.ToString("MM/dd/yyyy"),
        MeetTime.Equals(new DateTime(0)) ? "" : "\nВремя записи: " + MeetTime );
    
    
}