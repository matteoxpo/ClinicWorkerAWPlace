using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;

namespace Domain.Repositories.App.Role.Employee;

public interface IRegistrarRepository<ID> : IUserRepository<Registrar, ID> { }