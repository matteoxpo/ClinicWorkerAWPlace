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

    public string StringMeetingTime
    {
        get => Convert.ToString(MeetingTime, CultureInfo.InvariantCulture);
    }
    
    public  IEnumerable<RefForAnalysis> Analyzes;



    public Client(string name, string surname, DateTime meetingTime) : base(name, surname)
    {
        Complaints = new string("");
        Analyzes = new List<RefForAnalysis>();
        MeetingTime = meetingTime;

    }
}