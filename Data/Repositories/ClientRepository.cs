using Domain.Entities;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    // сделать приватным!

    private IAppointmentRepository _appointmentRepository;
    private ClientRepository(string pathToFile, IAppointmentRepository appointmentRepository) : base(pathToFile)
    {
        _appointmentRepository = appointmentRepository;
    }

    private static ClientRepository? globalRepositoryInstance;

    public static ClientRepository GetInstance()
    {
        return globalRepositoryInstance ??= new ClientRepository(
            "../../../../Data/DataSets/Client.json", AppointmentRepository.GetInstance());
    }

  


    public override bool CompareEntities(Client changedEntity, Client entity)
    {
        return (changedEntity.Id.Equals(entity.Id));
    }

    public void Update(Client changedClient)
    {
        Change(changedClient);
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
        var clients = DeserializationJson();
        foreach (var client in clients)
        {
            client.Appointments = _appointmentRepository.ReadByClient(client);
        }
        
        return clients;
    }
}