using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Reflection;

namespace Data.Repositories;

public abstract class BaseSQLiteRepository<DomainType>
{
    public BaseSQLiteRepository(SQLiteConnection dbConnection, string tableName)
    {
        TableName = new string(tableName) ?? throw new NullReferenceException("Name of table can't be null");
        _dbConnection = dbConnection;
    }
    protected readonly SQLiteConnection _dbConnection;

    public string TableName { get; set; }
    protected string _connectionString;
    public async Task AddRowAsync(DateTime time, string name, Dictionary<string, object> columnValues, string? tableName = null)
    {
        tableName ??= TableName;

        var columnNames = string.Join(", ", columnValues.Keys);
        var paramNames = string.Join(", ", columnValues.Keys.Select(key => $"@{key}"));

        using (var command = new SQLiteCommand($"INSERT INTO {tableName} ({columnNames}, {name}) VALUES ({paramNames}, @TimeValue)", _dbConnection))
        {
            foreach (var kvp in columnValues)
            {
                command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
            }

            command.Parameters.AddWithValue("@TimeValue", time);

            await command.ExecuteNonQueryAsync();
        }
    }

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

        string columns = string.Join(", ", columnValues.Keys.Where(k => columnValues[k] is not null));
        string values = string.Join(", ", columnValues.Keys.Where(k => columnValues[k] is not null).Select(k => "@" + k));

        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using (var command = new SQLiteCommand(query, _dbConnection))
        {
            foreach (var kvp in columnValues)
            {
                if (kvp.Value is not null)
                {
                    command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }
            }
            command.ExecuteNonQuery();
        }
    }

    protected async Task DeleteRowAsync(int id, string? tableName = null)
    {
        tableName ??= TableName;

        using (var command = new SQLiteCommand($"DELETE FROM {tableName} WHERE Id = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

    }
    protected async Task<ICollection<int>> ReadAllIdAsync(string? tableName = null)
    {
        tableName ??= TableName;

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

        return ids;
    }

    protected async Task<PrimitiveType> ReadPremitiveAsync<PrimitiveType>(string columnName, int id, string? tableName = null, string? idVariableName = null)
    {

        tableName ??= TableName;
        idVariableName ??= "Id";

        using (var command = new SQLiteCommand($"SELECT {columnName} FROM {tableName} WHERE {idVariableName} = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                if (!await reader.ReadAsync())
                {

                    throw new BaseSQLiteRepositoryException($"No data matches in table {tableName} with Id = {id}");
                }
                var converter = TypeDescriptor.GetConverter(typeof(PrimitiveType));
                var result = (PrimitiveType)converter.ConvertFromString(
                                reader[columnName].ToString() ?? throw new BaseSQLiteRepositoryException("Error reading")
                            );


                return result;
            }
        }
    }

    protected async Task<ICollection<PrimitiveType>> ReadPremitivesAsync<PrimitiveType>(string columnName, string? tableName = null)
    {

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


    public async Task UpdatePremitiveAsync(int id, string columnName, DateTime dateTime, string? tableName = null)
    {
        tableName ??= TableName;

        using (var command = new SQLiteCommand($"UPDATE {tableName} SET {columnName} = @newValue WHERE Id = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@newValue", dateTime);
            await command.ExecuteNonQueryAsync();
        }
    }

    protected async Task UpdatePremitiveAsync(int id, string columnName, string newValue, string? tableName = null)
    {
        tableName ??= TableName;

        using (var command = new SQLiteCommand($"UPDATE {tableName} SET {columnName} = @newValue WHERE Id = @id", _dbConnection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@newValue", newValue);
            await command.ExecuteNonQueryAsync();
        }
    }

    public class BaseSQLiteRepositoryException : Exception
    {
        public BaseSQLiteRepositoryException(string message) : base(message) { }
    }
}