namespace Domain.Entities.People;

[Serializable]
public class Client
{
    public string Name { get; set; }
    public string Surname { get ; set; }
    public  DateTime DateOfBirth { get; set; }
    public string Complaints { get; set; }
    public IEnumerable<ReferenceForAnalysis> Analyzes { get; set; }
    
    public string Id { get; set;} 
    
    
    
    [field: NonSerialized] public IEnumerable<Appointment> Appointments { get; set; }

    public Client()
    {
        Name = new string("name");
        Surname = new string("surname");
        Complaints = new string("complaints");
        Analyzes = new List<ReferenceForAnalysis>();
        Appointments = new List<Appointment>();
        Id = new string("0");
    }

    public Client(string name, string surname, DateTime birthTime, string complaints, IEnumerable<ReferenceForAnalysis> analyses, List<Appointment> appointments, string id) 
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Complaints = new string(complaints);
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Appointments = new List<Appointment>(appointments);
        Id = id;
    }
    public Client(string name, string surname, DateTime birthTime, string complaints, IEnumerable<ReferenceForAnalysis> analyses, string id) 
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = birthTime;
        Complaints = new string(complaints);
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Appointments = new List<Appointment>();
        Id = id;
    }

    public override string ToString() => string.Join(" ", Name, Surname, Id);
    
    
}