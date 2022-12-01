using System.Globalization;
using System.Reactive.Linq;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    private ClientRepository(string pathToFile) : base(pathToFile) { }

    private static ClientRepository? globalRepositoryInstance;

    public static ClientRepository GetInstance()
    { 
        return globalRepositoryInstance ??= new ClientRepository(
            "../../../../Data/DataSets/Clients.xml");
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

    public List<Client> Read()
    {
        return DeserializationJson(); 
    }

    public IObservable<Client> ObserveByNameSurnameDateOFBirth(string nameSurnameDate)
    {
        return AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => string.Equals(
                    emp.Name + emp.Surname + emp.DateOfBirth.ToString(CultureInfo.InvariantCulture), 
                    nameSurnameDate));
            }
        )!.Where<Client>((d) => !d.Equals(null));
    }

}
