// using System.Security.Cryptography.X509Certificates;
// using Domain.Entities.App;
// using Domain.Entities.App.Role;
// using Domain.Entities.Polyclinic.Appointment;

// using Domain.Repositories.App;

// using System.Reactive.Subjects;
// using Domain.Repositories.App.Role;
// using Microsoft.VisualBasic;
// using System.Reactive.Linq;
// using System.Runtime.Serialization;
// namespace Domain.UseCases.UserInteractor;
// public sealed class ClientUserInteractor<ID>
// {
//     private readonly IClientRepository _clientRepository;
//     private BehaviorSubject<IEnumerable<Client>> _clientsSubject;
//     public readonly IObservable<IEnumerable<Client>> ClientsObservable;

//     public ClientUserInteractor(IClientRepository clientRepository)
//     {
//         _clientRepository = clientRepository;
//         _clientsSubject = new BehaviorSubject<IEnumerable<Client>>(new List<Client>());
//         ClientsObservable = _clientsSubject.AsObservable();
//     }
//     public Client GetClient(string login, string password)
//     {
//         return _clientRepository.Read(login, password);
//     }

//     public ICollection<Appointment> GetAppointments(string login)
//     {
//         return _clientRepository.GetAppointments(login)
//             ?? throw new ClientUserInteractorException($"Cant find appointments of user with login {login}, the user dosn't exist!"); ;
//     }

// }

// public class ClientUserInteractorException : Exception
// {
//     public ClientUserInteractorException(string message) : base(message) { }
// }