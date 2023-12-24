using Domain.Entities.Polyclinic.Disease;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class DiseaseRepository : BaseSQLiteRepository<Disease>, IDiseaseRepository
{
    public DiseaseRepository(string connectionString, string tableName) : base(connectionString, tableName)
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
        return new Disease(
            await ReadPremitiveAsync<string>("Name", id),
            await ReadPremitiveAsync<string>("Description", id),
            TransmissionMapper.FromString(
                await ReadPremitiveAsync<string>("Transmission", id)
            ),
            id
        );
    }
}