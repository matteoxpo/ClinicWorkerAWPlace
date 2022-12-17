using Domain.Entities.Roles;

namespace Domain.Repositories;

public interface IDoctorRepository : IBasePerository<Doctor>
{
    IObservable<Doctor> ObserveByLogin(string login);
}