using Domain.Entities.Polyclinic.Drug;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class DrugRepository : BaseSQLiteRepository<Drug>, IDrugRepository
{
    public DrugRepository(string connectionString, string tableName) : base(connectionString, tableName)
    {
    }

    public async Task AddAsync(Drug entity)
    {
        await AddRowAsync(new Dictionary<string, object>()
        {
            {"Name", entity.Name},
            {"Description", entity.Description},
            {"Recipe", entity.Recipe}
        });
    }

    public override async Task<Drug?> ReadAsync(int id)
    {
        return new Drug(
            await ReadPremitiveAsync<string>("Name", id),
            await ReadPremitiveAsync<string>("Description", id),
            await ReadPremitiveAsync<bool>("Recipe", id),
            id
        );
    }
}