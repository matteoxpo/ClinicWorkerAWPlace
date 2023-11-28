using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;

namespace Domain.Repositories.App.Role.Employees;

public interface IJobTittleRepository<T, ID> : IBaseRepository<T, ID> where T : Employee { }

public interface IEmployeeRepository<ID> : IBaseRepository<Employee, ID>
{
    ICollection<IJobTittleRepository<Employee, ID>> _jobTittleRepositories { get; }
}
