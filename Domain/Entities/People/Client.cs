using System;
using System.Collections.Generic;
using System.Runtime;
using System.Data;
using System.Globalization;

namespace Domain.Entities.People;

[Serializable]
public class Client : Human
{
    public string Complaints;
    public List<KeyValuePair<int, DateTime>> Appointments;
    public string? CurrentDoctorMeetTime { get; set; }
    public List<RefForAnalysis> Analyzes;
    
    public Client() : base()
    {
        Complaints = new string("complaints");
        Appointments = new List<KeyValuePair<int, DateTime>>();
        Analyzes = new List<RefForAnalysis>();
    }

    public Client(string name, string surname, string complaints, List<RefForAnalysis> analyses, List<KeyValuePair<int, DateTime>> appointments) : base(name, surname)
    {
        Complaints = new string(complaints);
        Analyzes = new List<RefForAnalysis>(analyses);
        Appointments = appointments;
    }

    public Client(List<KeyValuePair<int, DateTime>> appointments) : base("name", "surname")
    {
        Appointments = appointments;
        Analyzes = new List<RefForAnalysis>();
        Complaints = new string("");
    }
}