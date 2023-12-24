using Domain.Entities.App.Role.Employees;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.App.Role.Employee;

public class AdministratorRepository : BaseSQLiteRepository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(string connectionString, string tableName) : base(connectionString, tableName)
    {
    }

    public IBenefitRepository BenefitRepository { get; }

    public IContactRepository ContactRepository { get; }

    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }
    public Task AddAsync(Administrator entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Administrator oldEntity)
    {
        throw new NotImplementedException();
    }

    public override Task<Administrator?> ReadAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Administrator nextEntity)
    {
        throw new NotImplementedException();
    }
}