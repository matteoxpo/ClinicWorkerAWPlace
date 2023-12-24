using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Building;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class AppoinmentRepository : BaseSQLiteRepository<Appointment>, IAppoinmentRepository
{
    public AppoinmentRepository(string connectionString, string tableName, IMedicineClinicRepository medicineClinicRepository, ICabinetRepository cabinetRepository) : base(connectionString, tableName)
    {
        MedicineClinicRepository = medicineClinicRepository;
        CabinetRepository = cabinetRepository;
    }

    public IMedicineClinicRepository MedicineClinicRepository { get; }
    public ICabinetRepository CabinetRepository { get; }


    public async Task AddAsync(Appointment entity)
    {
        await AddRowAsync(new Dictionary<string, object>
        {
            {"CabinetId", entity.MeetPlace.Cabinet.ID},
            {"ClinicId", entity.MeetPlace.Polyclinic.ID},
            {"EmployeeUserId", entity.DoctorID},
            {"ClientId", entity.ClientID},
            {"Date", entity.Date},
        });
    }

    public async Task DeleteAsync(Appointment oldEntity)
    {
        await DeleteRowAsync(oldEntity.ID);
    }

    public override async Task<Appointment> ReadAsync(int id)
    {
        return new Appointment(
            await ReadPremitiveAsync<int>("EmployeeUserId", id),
            await ReadPremitiveAsync<int>("ClientId", id),
            await ReadPremitiveAsync<DateTime>("Date", id),
            id,
            new MeetPlace(
                await MedicineClinicRepository.ReadAsync(
                    await ReadPremitiveAsync<int>("ClinicId", id)
                ),
                await CabinetRepository.ReadAsync(
                    await ReadPremitiveAsync<int>("CabinetId", id)
                )
             )
        );
    }

    public async Task UpdateAsync(Appointment nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID,
            new Dictionary<string, string>() {
                {"Description", nextEntity.Description ?? string.Empty},
                {"EmployeeUserId", nextEntity.DoctorID.ToString()},
                {"ClientId", nextEntity.ClientID.ToString()},
                {"Date", nextEntity.Date.ToString()}
            }
        );
    }
}