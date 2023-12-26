using System.Data.SQLite;
using Domain.Entities.Polyclinic.Disease;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class DiseaseRepository : BaseSQLiteRepository<Disease>, IDiseaseRepository
{
    public DiseaseRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public async Task AddAsync(Disease entity)
    {
        await AddRowAsync(new Dictionary<string, object>() {
            {"Name", entity.Name},
            {"Description", entity.Description},
            {"Transmission", entity.Transmission.ToString()}
        });
    }

    public override async Task<Disease?> ReadAsync(int id)
    {
        var transId = int.Parse(await ReadPremitiveAsync<string>("TransmissionId", id));
        var transmission = await ReadPremitiveAsync<string>("TransmissionType", transId, "DiseaseTransmission");

        return new Disease(
            await ReadPremitiveAsync<string>("Name", id),
            await ReadPremitiveAsync<string>("Description", id),
            TransmissionMapper.FromString(transmission),
            id
        );
    }
}