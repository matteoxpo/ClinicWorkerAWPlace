using System.Runtime;
using System.Data;

namespace Domain.Entities.People;

public class Client : Human
{

    public string Complaints;
    public DateTime MeetingTime;
    
    public List<RefForAnalysis> Analyzes;



    public Client(string name, string surname, DateTime meetingTime) : base(name, surname)
    {
        Complaints = new string("");
        Analyzes = new List<RefForAnalysis>();
        MeetingTime = meetingTime;

    }
}