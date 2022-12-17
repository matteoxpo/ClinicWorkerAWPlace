using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;

namespace Domain.Repositories;

public interface IAppointmentRepository : IBasePerository<Appointment>
{
    IEnumerable<Appointment> ReadByClient(Client client);
    IEnumerable<Appointment> ReadByClient(string passportSerial);
    IEnumerable<Appointment> ReadByDoctor(Doctor doctor);
    IEnumerable<Appointment> ReadByDoctor(string doctorLogin);

    IObservable<IEnumerable<Appointment>> ObserveByDoctor(string login);
    IObservable<IEnumerable<Appointment>> ObserveByDoctor(Doctor doctor);

}