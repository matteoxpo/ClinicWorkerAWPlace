using System.Data.SQLite;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Building;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class AppoinmentRepository : BaseSQLiteRepository<Appointment>, IAppoinmentRepository
{
    public AppoinmentRepository(SQLiteConnection dbConnection, string tableName, IMedicineClinicRepository medicineClinicRepository, ICabinetRepository cabinetRepository) : base(dbConnection, tableName)
    {
        MedicineClinicRepository = medicineClinicRepository;
        CabinetRepository = cabinetRepository;
    }

    public IMedicineClinicRepository MedicineClinicRepository { get; }
    public ICabinetRepository CabinetRepository { get; }


    public async Task AddAsync(Appointment entity)
    {
        await AddRowAsync(entity.Date, "Date", new Dictionary<string, object>
        {
            {"ClinicId", entity.MeetPlace.Polyclinic.ID},
            {"EmployeeUserId", entity.DoctorID},
            {"HumanUserId", entity.ClientID},
        });
    }

    public async Task DeleteAsync(Appointment oldEntity)
    {
        await DeleteRowAsync(oldEntity.ID);
    }

    public override async Task<Appointment> ReadAsync(int id)
    {
        var Clinic = await MedicineClinicRepository.ReadAsync(
                    await ReadPremitiveAsync<int>("ClinicId", id)
                );
        return new Appointment(
            await ReadPremitiveAsync<int>("EmployeeUserId", id),
            await ReadPremitiveAsync<int>("HumanUserId", id),
            await ReadPremitiveAsync<DateTime>("Date", id),
            id,
            new MeetPlace(
                Clinic,
                Clinic.Cabinets.First()
             )
        );
    }

    public async Task UpdateAsync(Appointment nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID, "Date", nextEntity.Date);
        await UpdatePremitiveAsync(nextEntity.ID,
            new Dictionary<string, string>() {
                {"Description", nextEntity.Description ?? string.Empty},
                {"EmployeeUserId", nextEntity.DoctorID.ToString()},
                {"ClientId", nextEntity.ClientID.ToString()}
            }
        );
    }
}