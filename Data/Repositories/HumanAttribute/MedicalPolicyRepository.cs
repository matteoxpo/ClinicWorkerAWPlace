using System.Data.SQLite;
using Domain.Entities.People.Attribute;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.HumanAttribute;

public class MedicalPolicyRepository : BaseSQLiteRepository<MedicalPolicy>, IMedicalPolicyRepository
{
    public MedicalPolicyRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public async Task AddAsync(MedicalPolicy entity)
    {
        await AddRowAsync(new Dictionary<string, object>()
        {
            {"Serial", entity.Serial},
            {"Number", entity.Number},
        });
        throw new NotImplementedException();
    }

    public override async Task<MedicalPolicy> ReadAsync(int id)
    {
        return new MedicalPolicy(
            await ReadPremitiveAsync<string>("Serial", id),
            await ReadPremitiveAsync<string>("Number", id),
            id
        );
    }

    public async Task UpdateAsync(MedicalPolicy nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID, new Dictionary<string, string>()
        {
            {"Serial", nextEntity.Serial},
            {"Number", nextEntity.Serial}
        });
    }
}