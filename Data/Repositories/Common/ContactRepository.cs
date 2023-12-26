using System.Data.SQLite;
using System.Security.Authentication;
using Domain.Entities.Common;
using Domain.Repositories.Common;

namespace Data.Repositories.Common;

public class ContactRepository : BaseSQLiteRepository<Contact>, IContactRepository
{
    public ContactRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public async Task AddAsync(Contact entity)
    {
        await AddRowAsync(new Dictionary<string, object>()
        {
            { "Email", entity.Email ?? string.Empty},
            { "PhoneNumber", entity.PhoneNumber ?? string.Empty},
        });
    }

    public async Task DeleteAsync(Contact oldEntity)
    {
        await DeleteRowAsync(oldEntity.ID);
    }

    public override async Task<Contact?> ReadAsync(int id)
    {
        return new Contact(
            await ReadPremitiveAsync<string>("PhoneNumber", id),
            await ReadPremitiveAsync<string>("Email", id),
            id
        );
    }

    public async Task UpdateAsync(Contact nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID, "Email", nextEntity.Email ?? string.Empty);
        await UpdatePremitiveAsync(nextEntity.ID, "PhoneNumber", nextEntity.PhoneNumber ?? string.Empty);
    }
}