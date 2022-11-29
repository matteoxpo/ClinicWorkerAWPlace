

using Domain.Entities;
using Domain.Entities.People;

namespace Data.Models;

[Serializable]
public class ClientDataStore
{
    public string Name;
    public string Surname;
    public string Complaints;
    public List<KeyValuePair<string, DateTime>> Appointments;
    public List<RefForAnalysisDataStore> Analyzes;

    public int Id;
    public ClientDataStore(string name, string surname, string complaints, List<KeyValuePair<string, DateTime>> appointments, List<RefForAnalysisDataStore> analyzes)
    {
        Name = name;
        Surname = surname;
        Complaints = complaints;
        Appointments = appointments;
        Analyzes = analyzes;
    }

    public ClientDataStore(Client client,IEnumerable<RefForAnalysisDataStore> refForAnalysisDataStores, List<KeyValuePair<string, DateTime>> appointments, int id)
    {
        Name = new string(client.Name);
        Surname = new string(client.Surname);
        Complaints = new string(client.Complaints);
        Analyzes = new List<RefForAnalysisDataStore>(refForAnalysisDataStores);
        Appointments = appointments;
        Id = id;
    }
    public ClientDataStore()
    {
        Appointments = new List<KeyValuePair<string, DateTime>>();
        Name = new string("name");
        Surname = new string("surname");
        Complaints = new string("complaints");
        Analyzes = new List<RefForAnalysisDataStore>();
        Id = 0;
    }

    public Client MapToClent()
    {
        var forAnalyses = new List<RefForAnalysis>();
        foreach (var analyz in Analyzes)
        {
            forAnalyses.Add(analyz.MapToRefForAnalyzis());
        }

        var doctors = new List<DoctorEmployee>();



        return new Client(Name, Surname, Complaints , forAnalyses, Appointments);
    }
    

}