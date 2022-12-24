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
        var clients = DeserializationJson();
        var refForAnalysis = new List<ReferenceForAnalysis>(_referenceForAnalysisRepository.Read());
        var clientAnalysis = new List<ReferenceForAnalysis>();
        foreach (var client in clients)
        {
            client.Appointments = _appointmentRepository.ReadByClient(client);
            clientAnalysis.AddRange(refForAnalysis.Where(refForAnalys => refForAnalys.ClientId.Equals(client.Id)));
            client.Analyzes = new List<ReferenceForAnalysis>(clientAnalysis);
            clientAnalysis.Clear();
        }

        return clients;
    }

    public static ClientRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new ClientRepository(
            "../../../../Data/DataSets/Client.json", AppointmentRepository.GetInstance(),
            ReferenceForAnalysisRepository.GetInstance());
    }
}