
using System.Data.SQLite;
using Domain.Entities.Polyclinic.Building;
using Domain.Repositories.Common;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;
public class CabinetRepository : BaseSQLiteRepository<Cabinet>, ICabinetRepository
{
    public CabinetRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public override async Task<Cabinet?> ReadAsync(int id)
    {
        return new Cabinet(
            await ReadPremitiveAsync<string>("Number", id),
            await ReadPremitiveAsync<string>("Type", id),
            id);
    }

    public async Task<IEnumerable<Cabinet>> ReadCabinetByClinicIdAsync(int id)
    {
        var cabs = new List<Cabinet>();
        using (var command = new SQLiteCommand($"SELECT Number, Type, Id FROM Cabinet WHERE ClinicId = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    cabs.Add(
                        new Cabinet(
                            reader["Number"].ToString() ?? throw new BaseSQLiteRepositoryException("Read cabinet number error"),
                            reader["Type"].ToString() ?? throw new BaseSQLiteRepositoryException("Read cabinet type error"),
                            int.Parse(reader["Id"].ToString() ?? throw new BaseSQLiteRepositoryException("Read cabinet id error"))
                        )
                    );
                }
            }
        }
        return cabs;
    }
}

public class MedicineClinicRepository : BaseSQLiteRepository<MedicineClinic>, IMedicineClinicRepository
{
    public MedicineClinicRepository(SQLiteConnection dbConnection, string tableName, ICabinetRepository cabinetRepository, IAddressRepository addressRepository, IContactRepository contactRepository) : base(dbConnection, tableName)
    {
        CabinetRepository = cabinetRepository;
        AddressRepository = addressRepository;
        ContactRepository = contactRepository;
    }

    public ICabinetRepository CabinetRepository { get; }
    public IContactRepository ContactRepository { get; }

    public IAddressRepository AddressRepository { get; }


    private async Task<ICollection<int>> GetCabinetsIds(int clinicId)
    {
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM Cabinet WHERE ClinicId = @clinicid", _dbConnection))
        {
            command.Parameters.AddWithValue("@clinicid", clinicId);
            using (var reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        return ids;
    }
    public override async Task<MedicineClinic?> ReadAsync(int id)
    {
        var cabinets = await CabinetRepository.ReadAsync(
                await GetCabinetsIds(id)
            );
        return new MedicineClinic(
            await AddressRepository.ReadAsync(await ReadPremitiveAsync<int>("AddressId", id)),
            await CabinetRepository.ReadCabinetByClinicIdAsync(id),
            await ContactRepository.ReadAsync(
                await ReadPremitiveAsync<int>("ContactId", id)
            ),
            id
        );
    }
}