namespace Domain.Entities.People;

[Serializable]
public class Client : Human
{
    public string Complaints { get; set; }
    public IEnumerable<ReferenceForAnalysis> Analyzes { get; set; }
    public IEnumerable<Tuple<DoctorEmployee, DateTime>> Doctors { get; set; }

    public Client() : base()
    {
        Complaints = new string("complaints");
        Analyzes = new List<ReferenceForAnalysis>();
    }

    public Client(string name, string surname, DateTime birthTime, string complaints, IEnumerable<ReferenceForAnalysis> analyses, List<Tuple<DoctorEmployee, DateTime>> doctors) :
        base(name, surname, birthTime)
    {
        Complaints = new string(complaints);
        Analyzes = new List<ReferenceForAnalysis>(analyses);
        Doctors = new List<Tuple<DoctorEmployee, DateTime>>(doctors);
    }
}