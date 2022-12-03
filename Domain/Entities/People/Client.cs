namespace Domain.Entities.People;

[Serializable]
public class Client : Human
{
    public string Complaints;
    public IEnumerable<RefForAnalysis> Analyzes;

    public Client() : base()
    {
        Complaints = new string("complaints");
        Analyzes = new List<RefForAnalysis>();
    }

    public Client(string name, string surname, DateTime birthTime, string complaints, IEnumerable<RefForAnalysis> analyses) :
        base(name, surname, birthTime)
    {
        Complaints = new string(complaints);
        Analyzes = new List<RefForAnalysis>(analyses);
    }
}