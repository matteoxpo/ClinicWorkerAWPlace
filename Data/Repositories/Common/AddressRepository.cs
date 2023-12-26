using System.Data.SQLite;
using Domain.Entities.Common;
using Domain.Repositories;
using Domain.Repositories.Common;

namespace Data.Repositories.Common;

public class AddressRepository : BaseSQLiteRepository<Address>, IAddressRepository
{
    public AddressRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public async Task AddAsync(Address entity)
    {
        await AddRowAsync(new Dictionary<string, object>()
        {
            {"Street",entity.Street },
            {"City",entity.City },
            {"State",entity.State },
            {"ZipCode",entity.ZipCode },
            {"Country",entity.Country },
        });
    }

    public override async Task<Address?> ReadAsync(int id)
    {
        return new Address(
            await ReadPremitiveAsync<string>("Street", id),
            await ReadPremitiveAsync<string>("City", id),
            await ReadPremitiveAsync<string>("State", id),
            await ReadPremitiveAsync<string>("ZipCode", id),
            await ReadPremitiveAsync<string>("Country", id),
            id
        );
    }
}