using Domain.Entities.Roles;
using Domain.Entities.Roles.Doctor;

namespace Domain.Repositories;

public interface IDoctorRepository : IBasePerository<Doctor>
{
    IObservable<Doctor> ObserveByLogin(string login);
}