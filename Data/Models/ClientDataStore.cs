

using Domain.Entities;
using Domain.Entities.People;

namespace Data.Models;

[Serializable]
public class ClientDataStore
{
    public string Name;
    public string Surname;
    public string Complaints;
    public DateTime BirthTime;
    public List<RefForAnalysisDataStore> Analyzes;

    public int Id;

    public ClientDataStore(Client client,IEnumerable<DoctorEmployeeDataStore> doctors,IEnumerable<RefForAnalysisDataStore> refForAnalysisDataStores, int id)
    {
        Name = new string(client.Name);
        Surname = new string(client.Surname);
        Complaints = new string(client.Complaints);
        BirthTime = client.DateOfBirth;
        Analyzes = new List<RefForAnalysisDataStore>(refForAnalysisDataStores);
        Id = id;
    }
    public ClientDataStore()
    {
        Name = new string("name");
        Surname = new string("surname");
        Complaints = new string("complaints");
        BirthTime = new DateTime(0);
        
        Analyzes = new List<RefForAnalysisDataStore>();
        Id = -1;
    }

    public Client MapToClent()
    {
        var forAnalyses = new List<RefForAnalysis>();
        foreach (var analyz in Analyzes)
        {
            forAnalyses.Add(analyz.MapToRefForAnalyzis());
        }

        return new Client(Name, Surname, Complaints, forAnalyses, BirthTime);
    }
    

}