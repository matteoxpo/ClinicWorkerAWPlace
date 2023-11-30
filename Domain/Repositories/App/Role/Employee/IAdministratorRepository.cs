using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;

namespace Domain.Repositories.App.Role;

public interface IAdministratorRepository<ID> : IUserRepository<Administrator, ID> { }