using System.Data.SQLite;
using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.App.Role.Employee;

public class DoctorRepository : BaseSQLiteRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(SQLiteConnection dbConnection,
                            string tableName,
                            IBenefitRepository benefitRepository,
                            IEducationRepository educationRepository,
                            IMedicalPolicyRepository medicalPolicyRepository,
                            IContactRepository contactRepository,
                            IAppoinmentRepository appoinmentRepository,
                            IAddressRepository addressRepository) : base(dbConnection, tableName)
    {
        BenefitRepository = benefitRepository;
        EducationRepository = educationRepository;
        MedicalPolicyRepository = medicalPolicyRepository;
        ContactRepository = contactRepository;
        AppoinmentRepository = appoinmentRepository;
        AddressRepository = addressRepository;
    }

    public IBenefitRepository BenefitRepository { get; }
    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }


    public IContactRepository ContactRepository { get; }

    public IAppoinmentRepository AppoinmentRepository { get; }

    public IAddressRepository AddressRepository { get; }

    public Task AddAsync(Doctor entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Doctor oldEntity)
    {
        throw new NotImplementedException();
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
    }

    private async Task<ICollection<int>> ReadAppointmentsIds(int humanUserId)
    {
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM Appointment WHERE HumanUserId = @humanuserid", _dbConnection))
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
    }

    public async override Task<Doctor> ReadAsync(int id)
    {
        return new Doctor(
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
            await BenefitRepository.ReadAsync(await ReadBenefitsIds(id)), "Field anavailable rn",
            await AppoinmentRepository.ReadAsync(await ReadAppointmentsIds(id))
        );
    }

    private async Task<IEnumerable<int>> ReadDoctorsIds()
    {

        string jobTitleQuery = "SELECT Id FROM JobTittle WHERE Name = 'Doctor'";
        var ids = new List<int>();

        using (SQLiteCommand jobTitleCommand = new SQLiteCommand(jobTitleQuery, _dbConnection))
        {
            // Получаем JobTittleId для должности 'Doctor'
            object jobTitleIdObj = jobTitleCommand.ExecuteScalar();

            if (jobTitleIdObj != null && jobTitleIdObj != DBNull.Value)
            {
                int jobTitleId = Convert.ToInt32(jobTitleIdObj);

                string employeeUserQuery = $"SELECT HumanUserId FROM EmployeeUser WHERE JobTittleId = {jobTitleId}";

                using (SQLiteCommand employeeUserCommand = new SQLiteCommand(employeeUserQuery, _dbConnection))
                {
                    using (SQLiteDataReader reader = employeeUserCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
        }
        return ids;
    }

    public override async Task<ICollection<Doctor>?> ReadAllAsync()
    {
        var domainTypeList = new List<Doctor>();
        foreach (var id in await ReadDoctorsIds())
        {
            var entity = await ReadAsync(id);
            if (entity is null)
            {
                throw new BaseSQLiteRepositoryException("Error of reading");
            }
            domainTypeList.Add(entity);
        }

        return domainTypeList.Count() != 0 ? domainTypeList : throw new BaseSQLiteRepositoryException($"Empty table {TableName}");
    }

    public Task UpdateAsync(Doctor nextEntity)
    {
        throw new NotImplementedException();
    }

    public class DoctorRepositoryException : Exception
    {
        public DoctorRepositoryException(string message) : base(message) { }
    }
}