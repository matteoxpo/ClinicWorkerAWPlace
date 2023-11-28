using Domain.Entities.App;
using Domain.Entities.App.Role;
using Domain.Repositories.App.Role;
using Domain.Repositories.App.Role.Employees;

namespace Domain.Repositories.App;

public interface IUserRepository<ID> : IBaseRepository<User, ID>
{
    IClientRepostory<ID> _clientRepostory { get; }
    IEmployeeRepository<ID> _employeeRepository { get; }
}
