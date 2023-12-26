using System.Data.SQLite;
using Data.Repositories.Polyclinic;
using Domain.Entities.App.Role;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Repositories.App.Role;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.App.Role;

public class ClientRepository : BaseSQLiteRepository<Client>, IClientRepository
{
    public ClientRepository(SQLiteConnection dbConnection, string tableName, IBenefitRepository benefitRepository, IContactRepository contactRepository, IEducationRepository educationRepository, IMedicalPolicyRepository medicalPolicyRepository, IAddressRepository addressRepository, IAppoinmentRepository appoinmentRepository, ITreatmentCourseRepositrory treatmentCourseRepositrory) : base(dbConnection, tableName)
    {
        BenefitRepository = benefitRepository;
        ContactRepository = contactRepository;
        EducationRepository = educationRepository;
        MedicalPolicyRepository = medicalPolicyRepository;
        AddressRepository = addressRepository;
        AppoinmentRepository = appoinmentRepository;
        TreatmentCourseRepositrory = treatmentCourseRepositrory;
    }
    public ITreatmentCourseRepositrory TreatmentCourseRepositrory { get; }
    public IAppoinmentRepository AppoinmentRepository { get; }
    public IBenefitRepository BenefitRepository { get; }

    public IContactRepository ContactRepository { get; }

    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }

    public IAddressRepository AddressRepository { get; }

    public Task AddAsync(Client entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Client oldEntity)
    {
        throw new NotImplementedException();
    }

    public ICollection<Appointment> GetAppointments(int id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Appointment> GetAppointments(string login)
    {
        throw new NotImplementedException();
    }

    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(string login)
    {
        throw new NotImplementedException();
    }

    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<Client> ReadAsync(int id)
    {
        var contactIdList = new List<int>();
        var educationIdList = new List<int>();
        var benefitIdList = new List<int>();
        var appointmentIdList = new List<int>();
        var treatmentCourseIdList = new List<int>();
        using (var command = new SQLiteCommand(
                                        $"SELECT HUC.Id AS HUCId, E.Id AS EducationId, HUB.Id AS BenefitId, TC.Id AS TreatmentCourseId, A.Id AS AppointmentId " +
                                        $"FROM HumanUserContact AS HUC " +
                                        $"LEFT JOIN Education AS E ON HUC.HumanUserId = E.HumanUserId " +
                                        $"LEFT JOIN HumanUserBenefit AS HUB ON HUC.HumanUserId = HUB.HumanUserId " +
                                        $"LEFT JOIN TreatmentCourse AS TC ON HUC.HumanUserId = TC.HumanUserId " +
                                        $"LEFT JOIN Appointment AS A ON HUC.HumanUserId = A.HumanUserId " +
                                        $"WHERE HUC.HumanUserId = @humanuserid", _dbConnection))

        {
            command.Parameters.AddWithValue("@humanuserid", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["HUCId"] is not null)
                    {
                        contactIdList.Add(int.Parse(reader["HUCId"].ToString()));
                    }
                    if (reader["EducationId"] is not null)
                    {
                        educationIdList.Add(int.Parse(reader["EducationId"].ToString()));
                    }
                    if (reader["BenefitId"] is not null)
                    {
                        benefitIdList.Add(int.Parse(reader["BenefitId"].ToString()));
                    }
                    if (reader["TreatmentCourseId"] is not null)
                    {
                        treatmentCourseIdList.Add(int.Parse(reader["TreatmentCourseId"].ToString()));
                    }
                    if (reader["AppointmentId"] is not null)
                    {
                        appointmentIdList.Add(int.Parse(reader["AppointmentId"].ToString()));
                    }
                }
            }
        }


        return new Client(
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
                await ReadPremitiveAsync<int>("MeducalPolicyId", id)
            ),
            await ContactRepository.ReadAsync(contactIdList),
            await EducationRepository.ReadAsync(educationIdList),
            await BenefitRepository.ReadAsync(benefitIdList),
            await AppoinmentRepository.ReadAsync(appointmentIdList),
            await TreatmentCourseRepositrory.ReadAsync(treatmentCourseIdList)
        );
    }

    public Task UpdateAsync(Client nextEntity)
    {
        throw new NotImplementedException();
    }
}