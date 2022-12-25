using Data.Models.People;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;

public class ClientRepository : BaseRepository<Client, ClientStorageModel>, IClientRepository
{
    private static ClientRepository? _globalRepositoryInstance;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IReferenceForAnalysisRepository _referenceForAnalysisRepository;

    private ClientRepository(string pathToFile, IAppointmentRepository appointmentRepository,
        IReferenceForAnalysisRepository referenceForAnalysisRepository) : base(pathToFile)
    {
        _appointmentRepository = appointmentRepository;
        _referenceForAnalysisRepository = referenceForAnalysisRepository;
    }


    public override bool CompareEntities(Client entity1, Client entity2)
    {
        return entity1.Id.Equals(entity2.Id);
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
        return DeserializationJson()
            .Select(client => new Client(
                client.Name,
                client.Surname,
                client.DateOfBirth,
                _referenceForAnalysisRepository.Read().Where(refForAnalys => refForAnalys.ClientId.Equals(client.Id)),
                _appointmentRepository.ReadByClient(client),
                client.Id,
                client.Complaints,
                client.MeetTime));
    }

    public static ClientRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new ClientRepository(
            "../../../../Data/DataSets/Client.json", AppointmentRepository.GetInstance(),
            ReferenceForAnalysisRepository.GetInstance());
    }
}