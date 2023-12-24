using Domain.Entities.People.Attribute;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.HumanAttribute;

public class EducationRepository : BaseSQLiteRepository<Education>, IEducationRepository
{
    public EducationRepository(string connectionString, string tableName) : base(connectionString, tableName)
    {
    }

    public async Task AddAsync(Education entity)
    {
        await AddRowAsync(new Dictionary<string, string>()
        {
            {"Serial", entity.Serial},
            {"Number", entity.Number},
            {"Description", entity.Description},
            {"Date", entity.Date.ToString()},
            {"HumanUserId", entity.HumanUserId.ToString()}
        });
    }

    public async Task DeleteAsync(Education oldEntity)
    {
        await DeleteRowAsync(oldEntity.ID);
    }

    public override async Task<Education> ReadAsync(int id)
    {
        return new Education(
            await ReadPremitiveAsync<string>("Number", id),
            await ReadPremitiveAsync<string>("Serial", id),
            await ReadPremitiveAsync<DateTime>("Date", id),
            await ReadPremitiveAsync<string>("Description", id),
            await ReadPremitiveAsync<int>("Description", id),
            id
        );
    }

    public async Task UpdateAsync(Education nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID, new Dictionary<string, string>(){
            {"Serial", nextEntity.Serial},
            {"Number", nextEntity.Serial},
            {"Description", nextEntity.Description},
            {"HumanUserID", nextEntity.HumanUserId.ToString()},
            {"Date", nextEntity.Date.ToString()}, // Danger zone plz just work fine
        });
    }
}