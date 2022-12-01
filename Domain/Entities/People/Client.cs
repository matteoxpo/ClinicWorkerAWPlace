using System;
using System.Collections.Generic;
using System.Runtime;
using System.Data;
using System.Globalization;

namespace Domain.Entities.People;

public class Client : Human
{
    public string Complaints;
    public List<RefForAnalysis> Analyzes;

    public Client() : base()
    {
        Complaints = new string("complaints");
        Analyzes = new List<RefForAnalysis>();
    }

    public Client(string name, string surname, string complaints, List<RefForAnalysis> analyses, DateTime birthTime) :
        base(name, surname, birthTime)
    {
        Complaints = new string(complaints);
        Analyzes = new List<RefForAnalysis>(analyses);
    }
}