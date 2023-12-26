using Domain.Entities.Common;
using Domain.Entities.App;
using Domain.Repositories.App;
using System.Data.SQLite;
using Domain.Repositories.Common;

namespace Data.Repositories.App;

public class AuthRepository : BaseSQLiteRepository<Auth>, IAuthRepository
{
    public AuthRepository(SQLiteConnection dbConnection, string tableName) : base(dbConnection, tableName)
    {
    }

    public async Task<bool> AuthAsync(string login, string password)
    {

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

                return result;
            }
        }
    }

    public async Task<int> GetIdByLoginAsync(string login)
    {

        using (var command = new SQLiteCommand($"SELECT Id, Login FROM {TableName} WHERE Login = @login", _dbConnection))
        {
            command.Parameters.AddWithValue("@login", login);
            using (var reader = command.ExecuteReader())
            {
                if (await reader.ReadAsync())
                {
                    var result = int.Parse(reader["Id"].ToString());

                    return result;
                }

                throw new BaseSQLiteRepositoryException($"Cant find user with login {login}");
            }
        }
    }

    private async Task<string> GetJobTittle(int id)
    {

        using (var command = new SQLiteCommand($"SELECT Login FROM EmployeeUser WHERE HumanUserId = @humanuserid", _dbConnection))
        {
            command.Parameters.AddWithValue("@humanuserid", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return reader["Name"].ToString();
                }
            }
        }
        return string.Empty;
    }

    private IEnumerable<string> GetJobNameByJobId(int id)
    {
        var res = new List<string>();
        using (var command = new SQLiteCommand($"SELECT Name FROM JobTittle WHERE Id = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    res.Add(
                        reader["Name"].ToString()
                    );
                }
            }
        }
        return res;
    }

    public async Task<bool> IsUserAsync<Type>(int id) where Type : User
    {

        using (var command = new SQLiteCommand($"SELECT JobTittleId FROM EmployeeUser WHERE HumanUserId = @humanuserid", _dbConnection))
        {
            command.Parameters.AddWithValue("@humanuserid", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var jobs = GetJobNameByJobId(int.Parse(reader["JobTittleId"].ToString()));
                    if (jobs.Contains(typeof(Type).Name))
                    {
                        return true;
                    }
                    // if (reader[nameof(Type)].ToString())
                }
            }
        }
        return false;
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