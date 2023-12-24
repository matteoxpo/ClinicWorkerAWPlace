using Domain.Entities.Common;
using Domain.Entities.App;
using Domain.Repositories.App;
using System.Data.SQLite;
using Domain.Repositories.Common;

namespace Data.Repositories.App;

public class AuthRepository : BaseSQLiteRepository<Auth>, IAuthRepository
{
    public AuthRepository(string connectionString, string tableName, IContactRepository contactRepository) : base(connectionString, tableName)
    {
        ContactRepository = contactRepository;
    }

    public IContactRepository ContactRepository { get; }

    public async Task<bool> AuthAsync(string login, string password)
    {
        await _dbConnection.OpenAsync();

        using (var command = new SQLiteCommand($"SELECT Login FROM {TableName} WHERE Password = @password AND Login = @login", _dbConnection))
        {
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@login", login);
            using (var reader = command.ExecuteReader())
            {
                bool result = false;
                if (await reader.ReadAsync())
                {
                    result = true;
                }
                await _dbConnection.CloseAsync();

                return result;
            }
        }
    }
    public async Task<IEnumerable<Contact>> GetContactsAsync(string login)
    {
        int[] ContactsIds;

        await _dbConnection.OpenAsync();
        using (var command = new SQLiteCommand($"SELECT Login FROM {TableName} WHERE Login = @login", _dbConnection))
        {
            command.Parameters.AddWithValue("@login", login);


            using (var reader = command.ExecuteReader())
            {
                if (!await reader.ReadAsync())
                {
                    throw new BaseSQLiteRepositoryException($"No User with login = {login}");
                }
                ContactsIds = (reader["Contact"].ToString() ?? throw new BaseSQLiteRepositoryException($"User {login} has no contacts")).Split(' ').Select(int.Parse).ToArray();
            }
        }
        await _dbConnection.CloseAsync();


        return await ContactRepository.ReadAsync(ContactsIds);
    }

    public override async Task<Auth?> ReadAsync(int id)
    {
        return new Auth(
            await ReadPremitiveAsync<string>("Login", id),
            await ReadPremitiveAsync<string>("Password", id)
        );
    }

    public async Task<bool> ResetPasswordAsync(string newPassword, int id)
    {
        await UpdatePremitiveAsync(id, "Password", newPassword);
        return await ReadPremitiveAsync<string>("Password", id) == newPassword;
    }
}