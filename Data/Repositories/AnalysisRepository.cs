using System.Data;
using System.Data.SQLite;
using System.Globalization;
using Dapper;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories;
public class AnalysisRepository : IAnalysisRepository<int>
{
    private readonly SQLiteConnection _dbConnection;

    public AnalysisRepository(string connectionString)
    {
        _dbConnection = new SQLiteConnection(connectionString);
        SqlMapper.AddTypeHandler(new TimeSpanHandler());
    }

    public Analysis? Read(int key)
    {
        throw new AnalysisRepositoryException("");
    }

    public ICollection<Analysis>? ReadAll()
    {
        try
        {
            _dbConnection.Open();

            string query = "SELECT Id, Type, Description, Peculiarities, TimeToTake, TimeToPrepeare FROM Analysis";
            var analyses = _dbConnection.Query<Analysis>(query).AsList();

            return analyses;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from database: {ex.Message}");
            return null;
        }
        finally
        {
            _dbConnection.Close();
        }
    }


    public class AnalysisRepositoryException : Exception
    {
        public AnalysisRepositoryException(string message) : base(message) { }
    }
}

public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
    {
        var stringValue = value.ToString();
        if (TimeSpan.TryParseExact(stringValue, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var timeSpan))
        {
            return timeSpan;
        }
        return TimeSpan.Zero;
    }

    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.Value = value.ToString(@"hh\:mm\:ss");
    }
}







//  _dbConnection.Open();
//     Analysis? analysis = null;
//     SQLiteCommand command = new SQLiteCommand(_dbConnection);
//     command.CommandText = "SELECT Type, Description, Peculiarities, TimeToTake, TimeToPrepeare FROM Analysis WHERE Id = @Id";
//     command.Parameters.Add(new SQLiteParameter("@Id", DbType.Int32) { Value = key });


//     SQLiteDataReader reader = command.ExecuteReader();
//     Console.WriteLine("!!");
//     if (reader.Read())
//     {
//         Console.WriteLine("??");
//         string type = reader["Type"].ToString() ?? throw new AnalysisRepositoryException("Type");
//         string description = reader["Description"].ToString() ?? throw new AnalysisRepositoryException("des");
//         string peculiarities = reader["Peculiarities"].ToString() ?? throw new AnalysisRepositoryException("pec");
//         TimeSpan timeToTake = TimeSpan.Parse(reader["TimeToTake"].ToString());
//         TimeSpan timeToPrepeare = TimeSpan.Parse(reader["TimeToPrepeare"].ToString());
//         Console.WriteLine(type);
//         Console.WriteLine(description);
//         Console.WriteLine(peculiarities);
//         Console.WriteLine(timeToTake);
//         Console.WriteLine(timeToPrepeare);

//         analysis = new Analysis(type, description, peculiarities, timeToTake, timeToPrepeare, key);
//     }
//     reader?.DisposeAsync();
//     command?.Dispose();
//     return analysis ?? throw new AnalysisRepositoryException("))");