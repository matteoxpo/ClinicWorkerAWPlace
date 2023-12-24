using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Reflection;

namespace Data.Repositories;

public abstract class BaseSQLiteRepository<DomainType>
{
    public BaseSQLiteRepository(string connectionString, string tableName)
    {
        _connectionString = new string(connectionString ?? throw new NullReferenceException("Connection string can't be null"));
        TableName = new string(tableName) ?? throw new NullReferenceException("Name of table can't be null");
        _dbConnection = new SQLiteConnection(_connectionString);
    }

    protected readonly SQLiteConnection _dbConnection;

    public string TableName { get; set; }
    protected string _connectionString;
    protected async Task AddRowAsync(Dictionary<string, object> columnValues, string? tableName = null)
    {
        var parsedDict = new Dictionary<string, string>();
        foreach (var el in columnValues)
        {
            parsedDict.Add(el.Key, el.Value.ToString() ?? throw new BaseSQLiteRepositoryException("Formatting obj to string error"));
        }
        await AddRowAsync(parsedDict, tableName);
    }
    protected async Task AddRowAsync(Dictionary<string, string> columnValues, string? tableName = null)
    {
        tableName ??= TableName;
        await _dbConnection.OpenAsync();
        string columns = string.Join(", ", columnValues.Keys);
        string values = string.Join(", ", columnValues.Keys.Select(k => "@" + k));

        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using (var command = new SQLiteCommand(query, _dbConnection))
        {
            foreach (var kvp in columnValues)
            {
                command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
            }

            command.ExecuteNonQuery();
        }

        await _dbConnection.CloseAsync();
    }
    protected async Task DeleteRowAsync(int id, string? tableName = null)
    {
        tableName ??= TableName;
        await _dbConnection.OpenAsync();
        using (var command = new SQLiteCommand($"DELETE FROM {tableName} WHERE Id = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        await _dbConnection.CloseAsync();
    }
    protected async Task<ICollection<int>> ReadAllIdAsync(string? tableName = null)
    {
        tableName ??= TableName;
        await _dbConnection.OpenAsync();
        var ids = new List<int>();

        using (var command = new SQLiteCommand($"SELECT Id FROM {tableName}", _dbConnection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }
        await _dbConnection.CloseAsync();
        return ids;
    }

    protected async Task<PrimitiveType> ReadPremitiveAsync<PrimitiveType>(string columnName, int id, string? tableName = null, string? idVariableName = null)
    {
        await _dbConnection.OpenAsync();
        tableName ??= TableName;
        idVariableName ??= "Id";

        using (var command = new SQLiteCommand($"SELECT {columnName} FROM {tableName} WHERE {idVariableName} = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                if (!await reader.ReadAsync())
                {
                    await _dbConnection.CloseAsync();
                    throw new BaseSQLiteRepositoryException($"No data matches in table {tableName} with Id = {id}");
                }
                var converter = TypeDescriptor.GetConverter(typeof(PrimitiveType));
                var result = (PrimitiveType)converter.ConvertFromString(
                                reader[columnName].ToString() ?? throw new BaseSQLiteRepositoryException("Error reading")
                            );
                await _dbConnection.CloseAsync();

                return result;
            }
        }
    }
    protected async Task<ICollection<PrimitiveType>> ReadPremitiveArrayFromColumnAsync<PrimitiveType>(string columnName, int id, string? tableName = null, string? idVariableName = null)
    {
        tableName ??= TableName;
        await _dbConnection.OpenAsync();
        idVariableName ??= "Id";

        var ids = new List<PrimitiveType>();

        using (var command = new SQLiteCommand($"SELECT {columnName} FROM {tableName} WHERE {idVariableName} = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteScalar() ?? throw new BaseSQLiteRepositoryException("");
            var splited = result.ToString().Split(' ');
            var converter = TypeDescriptor.GetConverter(typeof(PrimitiveType));

            foreach (var split in splited)
            {
                ids.Add((PrimitiveType)converter.ConvertFromString(split));
            }
        }

        await _dbConnection.CloseAsync();
        return ids;

    }
    protected async Task<ICollection<PrimitiveType>> ReadPremitivesAsync<PrimitiveType>(string columnName, string? tableName = null)
    {
        await _dbConnection.OpenAsync();
        tableName ??= TableName;
        var resultList = new List<PrimitiveType>();
        using (var command = new SQLiteCommand($"SELECT {columnName} FROM {tableName}", _dbConnection))
        {
            using (var reader = command.ExecuteReader())
            {
                var converter = TypeDescriptor.GetConverter(typeof(PrimitiveType));

                while (await reader.ReadAsync())
                {
                    var readed = reader[columnName] ?? throw new BaseSQLiteRepositoryException($"Reading error {columnName}");
                    resultList.Add((PrimitiveType)converter.ConvertFromString(readed.ToString()));
                }

            }
        }
        await _dbConnection.CloseAsync();
        return resultList;
    }
    public virtual async Task<ICollection<DomainType>?> ReadAllAsync()
    {
        var domainTypeList = new List<DomainType>();
        foreach (var id in await ReadAllIdAsync())
        {
            var entity = await ReadAsync(id);
            if (entity is null)
            {
                throw new BaseSQLiteRepositoryException("Error of reading");
            }
            domainTypeList.Add(entity);
        }

        return domainTypeList.Count() != 0 ? domainTypeList : throw new BaseSQLiteRepositoryException($"Empty table {TableName}");
    }
    public abstract Task<DomainType> ReadAsync(int id);
    protected async Task UpdatePremitiveAsync(int id, Dictionary<string, string> columnValues, string? tableName = null)
    {
        foreach (var el in columnValues)
        {
            await UpdatePremitiveAsync(id, el.Key, el.Value, tableName);
        }
    }
    protected async Task UpdatePremitiveAsync(int id, Dictionary<string, object> columnValues, string? tableName = null)
    {
        var parsedDict = new Dictionary<string, string>();
        foreach (var el in columnValues)
        {
            parsedDict.Add(el.Key, el.Value.ToString());
        }
        await UpdatePremitiveAsync(id, parsedDict, tableName);
    }
    protected async Task UpdatePremitiveAsync(int id, string columnName, object premitiveValue, string? tableName = null)
    {
        await UpdatePremitiveAsync(id, columnName, premitiveValue.ToString() ?? throw new BaseSQLiteRepositoryException("Value cannot be converted to string"), tableName);
    }
    protected async Task UpdatePremitiveAsync(int id, string columnName, string newValue, string? tableName = null)
    {
        tableName ??= TableName;
        using (var command = new SQLiteCommand($"UPDATE {tableName} SET {columnName} = @newValue WHERE Id = @id", _dbConnection))
        {
            await _dbConnection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@newValue", newValue);
            await command.ExecuteNonQueryAsync();
            await _dbConnection.CloseAsync();
        }
    }

    public class BaseSQLiteRepositoryException : Exception
    {
        public BaseSQLiteRepositoryException(string message) : base(message) { }
    }
}