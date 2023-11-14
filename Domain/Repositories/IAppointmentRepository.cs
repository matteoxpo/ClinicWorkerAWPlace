using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Role;
using Domain.Entities.Role.Doctor;

namespace Domain.Repositories;

public interface IAppointmentRepository : IBasePerository<Appointment>
{
    IEnumerable<Appointment> ReadByClient(Client client);
    IEnumerable<Appointment> ReadByClient(string Id);
    IEnumerable<Appointment> ReadByDoctor(Doctor doctor);
    IEnumerable<Appointment> ReadByDoctor(string doctorLogin);
}