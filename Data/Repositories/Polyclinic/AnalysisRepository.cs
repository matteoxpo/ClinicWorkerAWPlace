using Domain.Entities.Polyclinic.Analysis;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;
public class AnalysisRepository : BaseSQLiteRepository<Analysis>, IAnalysisRepository
{
    public AnalysisRepository(string connectionString, string tableName = "Analysis") : base(connectionString, tableName)
    {

    }
    public async override Task<Analysis?> ReadAsync(int id)
    {
        return new Analysis(
          await ReadPremitiveAsync<string>("Type", id),
          await ReadPremitiveAsync<string>("Description", id),
          await ReadPremitiveAsync<string>("Peculiarities", id),
          id
       );
    }
    public class AnalysisRepositoryException : Exception
    {
        public AnalysisRepositoryException(string message) : base(message) { }
    }
}