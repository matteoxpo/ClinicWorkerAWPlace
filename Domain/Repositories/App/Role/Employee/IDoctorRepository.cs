using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Repositories.Polyclinic;

namespace Domain.Repositories.App.Role.Employee;

public interface IDoctorRepository : IUserRepository<Doctor>, IReadaleAll<Doctor>
{
    IAppoinmentRepository AppoinmentRepository { get; }
}
