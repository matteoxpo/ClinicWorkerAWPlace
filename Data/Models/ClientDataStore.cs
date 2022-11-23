

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
    public IEnumerable<DoctorEmployee> Doctors;
    public IEnumerable<RefForAnalysisDataStore> Analyzes;

    public int Id;

    public ClientDataStore(Client client, int id)
    {
        Name = new string(client.Name);
        Surname = new string(client.Surname);
        Complaints = new string(client.Complaints);
        MeetingTime = client.MeetingTime;
        Doctors = new List<DoctorEmployee>(client.Doctors);
        Analyzes = new List<RefForAnalysisDataStore>(client.Analyzes);
        Id = id;
    }

    public ClientDataStore()
    {
        Name = new string("name");
        Surname = new string("surname");
        Complaints = new string("complaints");
        
        MeetingTime = new DateTime(0);
        Doctors = new List<DoctorEmployee>();
        Analyzes = new List<RefForAnalysisDataStore>();
        Id = 0;
    }
    

}