using System;
using System.Collections.Generic;
using System.Runtime;
using System.Data;
using System.Globalization;

namespace Domain.Entities.People;

public class Client : Human
{
    public string Complaints;
    public DateTime MeetingTime;
    public IEnumerable<DoctorEmployee> Doctors;
    public IEnumerable<RefForAnalysis> Analyzes;

    public Client(string name, string surname, string complaints,DateTime meetingTime, IEnumerable<RefForAnalysis> analyses,IEnumerable<DoctorEmployee> doctors) : base(name, surname)
    {
        Complaints = new string(complaints);
        Analyzes = new List<RefForAnalysis>(analyses);
        MeetingTime = meetingTime;
        Doctors = doctors;
    }
    
    public Client(string name, string surname, DateTime meetingTime) : base(name, surname)
    {
        Complaints = new string("");
        Analyzes = new List<RefForAnalysis>();
        MeetingTime = meetingTime;
        Doctors = new List<DoctorEmployee>();
    }
    
    public Client() : base("name", "surname")
    {
        Analyzes = new List<RefForAnalysis>();
        Complaints = new string("");
        Doctors = new List<DoctorEmployee>();
    }
}