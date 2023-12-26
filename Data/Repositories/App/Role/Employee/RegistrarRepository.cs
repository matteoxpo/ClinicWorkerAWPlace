using System.Data;
using System.Data.SQLite;
using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.App;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.App.Role.Employee;

public class RegistrarRepository : BaseSQLiteRepository<Registrar>, IRegistrarRepository
{
    public RegistrarRepository(SQLiteConnection dbConnection, string tableName, IBenefitRepository benefitRepository, IContactRepository contactRepository, IEducationRepository educationRepository, IMedicalPolicyRepository medicalPolicyRepository, IAddressRepository addressRepository) : base(dbConnection, tableName)
    {
        BenefitRepository = benefitRepository;
        ContactRepository = contactRepository;
        EducationRepository = educationRepository;
        MedicalPolicyRepository = medicalPolicyRepository;
        AddressRepository = addressRepository;
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

        await AddRowAsync(new Dictionary<string, string>()
        {
            {"SalaryPerHour", entity.SalaryPerHour.ToString()},
            {"JobTittleId", (await GetRegistrarId()).ToString()},
            {"HumanUserId", entity.ID.ToString()},
        }, "EmployeeUser");
        throw new NotImplementedException();
    }

    private async Task<int> GetRegistrarId()
    {
        using (var command = new SQLiteCommand($"SELECT Name, Id FROM JobTittle", _dbConnection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    var readed = reader["Name"].ToString();
                    if (readed == "Registrar")
                    {
                        return int.Parse(reader["Id"].ToString());
                    }
                }
            }
        }
        return -1;
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
                await ReadPremitiveAsync<int>("AddressId", id)
            ),
            await ReadPremitiveAsync<DateTime>("DateOfBirth", id),
            SexMapper.FromString(
                await ReadPremitiveAsync<string>("Sex", id)
            ),
            id,
            await MedicalPolicyRepository.ReadAsync(
                await ReadPremitiveAsync<int>("MedicalPolicyId", id)
            ),
            await ContactRepository.ReadAsync(await ReadContactsIds(id)),
            await EducationRepository.ReadAsync(await ReadEducationsIds(id)),
            await ReadPremitiveAsync<decimal>("SalaryPerHour", id, "EmployeeUser", "HumanUserId"),
            await BenefitRepository.ReadAsync(await ReadBenefitsIds(id)),
            "Field is unavailable for now"
        );
    }

    private async Task<ICollection<int>> ReadEducationsIds(int humanUserId)
    {
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM Education WHERE HumanUserId = @humanuserid", _dbConnection))
        {
            command.Parameters.AddWithValue("@humanuserid", humanUserId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        return ids;
    }

    private async Task<ICollection<int>> ReadBenefitsIds(int humanUserId)
    {
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM HumanUserBenefit WHERE HumanUserId = @humanuserid", _dbConnection))
        {
            command.Parameters.AddWithValue("@humanuserid", humanUserId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        return ids;
    }

    private async Task<ICollection<int>> ReadContactsIds(int humanUserId)
    {
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM HumanUserContact WHERE HumanUserId = @humanuserid", _dbConnection))
        {
            command.Parameters.AddWithValue("@humanuserid", humanUserId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        return (ids);

        // await Read
    }
    public async Task UpdateAsync(Registrar nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID,
        new Dictionary<string, string>()
        {
            {"Login", nextEntity.Login},
            {"Password", nextEntity.Password},
            {"Name", nextEntity.Name},
            {"Surname", nextEntity.Surname},
            {"PatronymicName", nextEntity.PatronymicName},
            {"AddressId", nextEntity.Address.ID.ToString()},
            {"Sex", nextEntity.Sex.ToString()},
            {"MedicalPolicyId", nextEntity.Policy.ID.ToString()},
        });
        await UpdatePremitiveAsync(nextEntity.ID, "DateOfBirth", nextEntity.DateOfBirth);
        // ContactRepository.Update(nextEntity.Contacts);
    }
}