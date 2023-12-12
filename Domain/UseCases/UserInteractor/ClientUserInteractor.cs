using System.Security.Cryptography.X509Certificates;
using Domain.Entities.App;
using Domain.Entities.App.Role;
using Domain.Entities.Polyclinic.Appointment;

using Domain.Repositories.App;

using System.Reactive.Subjects;
using Domain.Repositories.App.Role;
using Microsoft.VisualBasic;
using System.Reactive.Linq;
using System.Runtime.Serialization;
namespace Domain.UseCases.UserInteractor;
public sealed class ClientUserInteractor<ID>
{
    private readonly IClientRepository<ID> _clientRepository;
    private BehaviorSubject<IEnumerable<Client>> _clientsSubject;
    public readonly IObservable<IEnumerable<Client>> ClientsObservable;

    public ClientUserInteractor(IClientRepository<ID> clientRepository)
    {
        _clientRepository = clientRepository;
        _clientsSubject = new BehaviorSubject<IEnumerable<Client>>(new List<Client>());
        ClientsObservable = _clientsSubject.AsObservable();
    }
    // public void AddAppointment(string login, Appointment newAppointment)
    // {
    //     //TODO: validate time
    //     // observers.First().OnNext();
    //     var t = _clientsSubject.TryGetValue(out var clients);
    //     foreach (var client in clients)
    //     {
    //         if (client.Login == login)
    //         {
    //             client.AddAppointment(newAppointment);
    //             // update in repo
    //             _clientsSubject.OnNext(_clientRepository.Read());
    //         }
    //     }

    //     // foreach (var sub in _clientSubjects)
    //     // {
    //     //     try
    //     //     {
    //     //         var t = sub.TryGetValue(out var client);
    //     //         if (client is not null && string.Equals(client.Login, login))
    //     //         {
    //     //             client.AddAppointment(newAppointment);
    //     //         }
    //     //     }
    //     //     catch
    //     //     {
    //     //         // ignored
    //     //     }
    //     // }

    // }
    public Client GetClient(string login, string password)
    {
        return _clientRepository.Read(login, password);
    }

    public ICollection<Appointment> GetAppointments(string login)
    {
        return _clientRepository.GetAppointments(login)
            ?? throw new ClientUserInteractorException($"Cant find appointments of user with login {login}, the user dosn't exist!"); ;
    }
    public IObservable<Client> GetObservable(string login)
    {
        throw new Exception();
    }


}

public class ClientUserInteractorException : Exception
{
    public ClientUserInteractorException(string message) : base(message) { }
}