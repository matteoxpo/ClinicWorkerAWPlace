using System.Data.SQLite;
using Data.Repositories;
internal class Program
{
    private static void Main(string[] args)
    {
        TestDb().GetAwaiter().GetResult();
    }
    private static async Task TestDb()
    {
        string pathToDatabase = @"F:\ProgramFiles\Programming\ClinicWorkerAWPlace\Data\ClinicWorkerAWPlace.db";
        string connectionString = $"Data Source={pathToDatabase};Version=3;";

        var dbConnection = new SQLiteConnection(connectionString);
        dbConnection.Open();
        using (var command = new SQLiteCommand("SELECT DiseaseId FROM TreatmentPart WHERE Id = @id", dbConnection))
        {
            command.Parameters.AddWithValue("@id", 1);
            using (var reader = command.ExecuteReader())
            {
                if (!await reader.ReadAsync())
                {
                    await dbConnection.CloseAsync();
                }

                var result = reader["DiseaseId"].ToString();
                await dbConnection.CloseAsync();
            }
        }
    }
}

// var analysisRepository = new AnalysisRepository(connectionString);
// var res = analysisRepository.ReadAll();