using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.App;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.App.Role.Employee;

public class RegistrarRepository : BaseSQLiteRepository<Registrar>, IUserRepository<Registrar>
{
    public RegistrarRepository(string connectionString, string tableName) : base(connectionString, tableName)
    {
    }

    public IBenefitRepository BenefitRepository { get; }

    public IContactRepository ContactRepository { get; }

    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }
    public IAddressRepository AddressRepository { get; }


    public async Task AddAsync(Registrar entity)
    {
        await ContactRepository.AddAsync(entity.Contacts);
        await EducationRepository.AddAsync(entity.Education);
        await MedicalPolicyRepository.AddAsync(entity.Policy);

        await AddRowAsync(new Dictionary<string, string>() {
            {"Login", entity.Login},
            {"Password", entity.Password},
            {"Name", entity.Name},
            {"Surname", entity.Surname},
            {"PatronymicName", entity.PatronymicName},
            {"DateOfBirth", entity.DateOfBirth.ToString()},
            {"Sex", entity.Sex.ToString()},
            {"InsurancyPolicyId", entity.Policy.ID.ToString()},
            {"AddressId", entity.Address.ID.ToString()},
            {"ContacId", string.Join(",", entity.Contacts.Select(contact => contact.ID))},
            {"BenefitId", string.Join(",", entity.Benefits.Select(benefit => benefit.ID)) },
            {"Description", entity.Description },
         }, "HumanUser");

        await AddRowAsync(new Dictionary<string, object>()
        {
            {"SalaryPerHour", entity.SalaryPerHour},
            {"StartWorkDate", entity.DateOfEmployment},
        }, "EmployeeUser");
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Registrar oldEntity)
    {
        throw new NotImplementedException("Needs to delete data from all tables in future");
        // await DeleteRow(oldEntity.ID);
    }

    public override async Task<Registrar> ReadAsync(int id)
    {
        return new Registrar(
            await ReadPremitiveAsync<string>("Login", id),
            await ReadPremitiveAsync<string>("Password", id),
            await ReadPremitiveAsync<string>("Name", id),
            await ReadPremitiveAsync<string>("Surname", id),
            await ReadPremitiveAsync<string>("PatronymicName", id),
            await AddressRepository.ReadAsync(
                await ReadPremitiveAsync<int>("AddresId", id)
            ),
            await ReadPremitiveAsync<DateTime>("DateOfBirth", id),
            SexMapper.FromString(
                await ReadPremitiveAsync<string>("Sex", id)
            ),
            id,
            await MedicalPolicyRepository.ReadAsync(
                await ReadPremitiveAsync<int>("InsurancyPolicyId", id)
            ),
            await ContactRepository.ReadAsync(
                await ReadPremitiveArrayFromColumnAsync<int>("ContactId", id)
            ),
            await EducationRepository.ReadAsync(
                await ReadPremitiveArrayFromColumnAsync<int>("EducationId", id)
            ),
            await ReadPremitiveAsync<decimal>("SalaryPerHour", id, "EmployeeUser", "HumanUserId"),
            await ReadPremitiveAsync<DateTime>("StartWorkDate", id, "EmployeeUser", "HumanUserId"),
            await BenefitRepository.ReadAsync(
                await ReadPremitiveArrayFromColumnAsync<int>("BenefitId", id)
            ),
            await ReadPremitiveAsync<string>("Description", id)
        );
    }

    public Task UpdateAsync(Registrar nextEntity)
    {
        throw new NotImplementedException();
    }
}