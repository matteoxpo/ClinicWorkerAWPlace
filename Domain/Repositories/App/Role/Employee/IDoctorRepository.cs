using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;

namespace Domain.Repositories.App.Role.Employee;

public interface IDoctorRepository<ID> : IUserRepository<Doctor, ID> { }