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
    public List<KeyValuePair<string, DateTime>> Appointments;
    public List<RefForAnalysis> Analyzes;
    
    public Client() : base()
    {
        Complaints = new string("complaints");
        Appointments = new List<KeyValuePair<string, DateTime>>();
        Analyzes = new List<RefForAnalysis>();
    }

    public Client(string name, string surname, string complaints, List<RefForAnalysis> analyses, List<KeyValuePair<string, DateTime>> appointments) : base(name, surname)
    {
        Complaints = new string(complaints);
        Analyzes = new List<RefForAnalysis>(analyses);
        Appointments = appointments;
    }

    public Client(List<KeyValuePair<string, DateTime>> appointments) : base("name", "surname")
    {
        Appointments = appointments;
        Analyzes = new List<RefForAnalysis>();
        Complaints = new string("");
    }
}