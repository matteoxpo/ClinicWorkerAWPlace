

using Domain.Entities;
using Domain.Entities.People;

namespace Data.Models;

[Serializable]
public class ClientDataStore
{
    public string Name;
    public string Surname;
    public string Complaints;
    public DateTime MeetingTime;
    public List<DoctorEmployeeDataStore> Doctors;
    public List<RefForAnalysisDataStore> Analyzes;

    public int Id;

    public ClientDataStore(Client client,IEnumerable<DoctorEmployeeDataStore> doctors,IEnumerable<RefForAnalysisDataStore> refForAnalysisDataStores, int id)
    {
        Name = new string(client.Name);
        Surname = new string(client.Surname);
        Complaints = new string(client.Complaints);
        MeetingTime = client.MeetingTime;
        Doctors = new List<DoctorEmployeeDataStore>(doctors);
        Analyzes = new List<RefForAnalysisDataStore>(refForAnalysisDataStores);
        Id = id;
    }
    public ClientDataStore()
    {
        Name = new string("name");
        Surname = new string("surname");
        Complaints = new string("complaints");
        
        MeetingTime = new DateTime(0);
        Doctors = new List<DoctorEmployeeDataStore>();
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
        foreach (var doc in Doctors)
        {
            doctors.Add(doc.MapToDoctorEmployee());
        }


        return new Client(Name, Surname, Complaints, MeetingTime, forAnalyses, doctors);
    }
    

}