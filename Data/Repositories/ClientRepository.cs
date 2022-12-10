using System.Globalization;
using System.Reactive.Linq;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    // сделать приватным!
    public ClientRepository(string pathToFile) : base(pathToFile) { }

    private static ClientRepository? globalRepositoryInstance;

    public static ClientRepository GetInstance()
    { 
        return globalRepositoryInstance ??= new ClientRepository(
            "../../../../Data/DataSets/Patients.json");
    }
    

    protected override bool CompareEntities(Client changedEntity, Client entity)
    {
        return (string.Equals(changedEntity.Surname, entity.Surname) && string.Equals(changedEntity.Name, entity.Name));
    }

    public void Update(Client newClient)
    {
        Change(newClient);
    }

    public void Delete(Client newClient)
    {
        Remove(newClient);
    }

    public void Add(Client newClient)
    {
        Append(newClient);
    }

    public IEnumerable<Client> Read()
    {
        var clients =  DeserializationJson();
        foreach (var client in clients)
        {
            if (client.Analyzes is null)
            {
                client.Analyzes = new List<ReferenceForAnalysis>();
            }

            if (client.Doctors is null)
            {
                client.Doctors = new List<Tuple<DoctorEmployee, DateTime>>();
            }
        }

        return clients;
    }

   

}
