using Domain.Entities.Role;
using Domain.Entities.Role.Doctor;

namespace Domain.Repositories;

public interface IDoctorRepository : IBasePerository<Doctor>
{
    IObservable<Doctor> ObserveByLogin(string login);
}