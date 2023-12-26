using System.Data.SQLite;
using System.Linq.Expressions;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class ReferralForAnalysisRepository : BaseSQLiteRepository<ReferralForAnalysis>, IReferralForAnalysisRepository
{
    public ReferralForAnalysisRepository(SQLiteConnection dbConnection, string tableName, IAnalysisRepository analysisRepository) : base(dbConnection, tableName)
    {
        AnalysisRepository = analysisRepository;
    }

    public IAnalysisRepository AnalysisRepository { get; }

    public async Task AddAsync(ReferralForAnalysis entity)
    {
        await AddRowAsync(new Dictionary<string, object>()
        {
            {"AnalysisId", entity.Analysis.ID},
            {"HumanUserId", entity.ClientID },
            {"HumanUserId", entity.DoctorID },
            {"Description", entity.Description},
            {"Result", entity.Results ?? string.Empty},
            {"Date", entity.Date}
        });
    }

    public async Task DeleteAsync(ReferralForAnalysis oldEntity)
    {
        await DeleteRowAsync(oldEntity.ID);
    }

    public override async Task<ReferralForAnalysis?> ReadAsync(int id)
    {
        return new ReferralForAnalysis(
            await AnalysisRepository.ReadAsync(
                await ReadPremitiveAsync<int>("AnalysisId", id)
            ),
            await ReadPremitiveAsync<int>("HumanUserId", id),
            await ReadPremitiveAsync<int>("EmployeeUserId", id),
            await ReadPremitiveAsync<DateTime>("Date", id),
            await ReadPremitiveAsync<string>("Description", id),
            id,
            await ReadPremitiveAsync<string>("Result", id)
        );
    }

    public async Task UpdateAsync(ReferralForAnalysis nextEntity)
    {
        await UpdatePremitiveAsync(nextEntity.ID, new Dictionary<string, string>()
        {
            {"AnalysisId", nextEntity.Analysis.ID.ToString()},
            {"DoctorId", nextEntity.DoctorID.ToString()},
            {"HumanUserId", nextEntity.ClientID.ToString()},
            {"Date", nextEntity.Date.ToString()},
            {"Description", nextEntity.Description},
            {"Result", nextEntity.Results ?? string.Empty},
        });
    }
}