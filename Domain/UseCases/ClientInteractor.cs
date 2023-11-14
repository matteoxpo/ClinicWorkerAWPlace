using Domain.Entities.People;
using Domain.Entities.Role;
using Domain.Entities.Role.Doctor;
using Domain.Repositories;

namespace Domain.UseCases;

public class ClientInteractor
{
    private readonly IClientRepository _clientRepository;

    public ClientInteractor(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Client Get(string id)
    {
        return _clientRepository.Read().First(d => d.Id.Equals(id));
    }

    public void Add(Client newClient)
    {
        foreach (var client in _clientRepository.Read())
            if (_clientRepository.CompareEntities(client, newClient))
                throw new ClientException(ClientException.ClientIsAlreadyInBase);

        _clientRepository.Add(newClient);
    }

    public IEnumerable<Client> Get(string name, string surname)
    {
        var clients = new List<Client>();
        foreach (var patient in _clientRepository.Read())
            if (string.Equals(patient.Name, name) && string.Equals(patient.Surname, surname))
                clients.Add(patient);

        return clients;
    }

    public IEnumerable<Client> Get()
    {
        return _clientRepository.Read();
    }

    public IEnumerable<Client> Get(IEnumerable<string> clientsId)
    {
        var clients = new List<Client>();

        foreach (var patient in new List<Client>(_clientRepository.Read()))
            if (new List<string>(clientsId).Any(id => id.Equals(patient.Id)))
                clients.Add(patient);

        return clients;
    }


    public IEnumerable<Client> GetDoctorsPatients(Doctor doctor)
    {
        return Get(doctor.Appointments.Select(appointment => appointment.ClientId).ToList());
    }
}

public class ClientException : Exception
{
    public ClientException(string message) : base(message)
    {
    }

    public static string ClientIsAlreadyInBase => "Человек с таким номером и серией паспорта уже существует";
    public static string BusyClient => "Клиент уже записан на это время";
    public static string PastTime => "Время не соттветствует текущему";
}