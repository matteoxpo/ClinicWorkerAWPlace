using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using Domain.Entities.Polyclinic.Treatment;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class TreatmentStageRepository : BaseSQLiteRepository<TreatmentStage>, ITreatmentStageRepository
{
    public IDrugRepository DrugRepository { get; }

    public IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }

    public TreatmentStageRepository(string connectionString, string tableName, IDrugRepository drugRepository, IReferralForAnalysisRepository referralForAnalysisRepository) : base(connectionString, tableName)
    {
        DrugRepository = drugRepository;
        ReferralForAnalysisRepository = referralForAnalysisRepository;
    }

    public async Task AddAsync(TreatmentStage entity)
    {
        foreach (var anal in entity.Analyses)
        {
            await AddRowAsync(new Dictionary<string, string>() {
                {"TreatmentStageId", entity.ID.ToString()},
                {"ReferralForAnalysisId", anal.ID.ToString()},
             },
            "TreatmentStageReferralForAnalysis");
        }
        foreach (var drug in entity.Drug)
        {
            await AddRowAsync(new Dictionary<string, string>() {
                {"TreatmentStageId", entity.ID.ToString()},
                {"ReferralForAnalysisId", drug.ID.ToString()},
             },
            "TreatmentStageDrug");
        }

        await AddRowAsync(
            new Dictionary<string, object>() {
                {"EmployeeUserId",entity.DoctorId},
                {"Description", entity.Description},
                {"TreatmentCourseId", entity.ID}
            }
        );
    }
    private async Task<ICollection<int>> ReadIdsAsync(int stageId, string variable, string tableName)
    {
        var drugsIs = new List<int>();
        await _dbConnection.OpenAsync();
        using (var command = new SQLiteCommand($"SELECT {variable} FROM {tableName} WHERE TreatmentStageId = @stageId", _dbConnection))
        {
            command.Parameters.AddWithValue("@stageId", stageId);
            using (var reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    drugsIs.Add(int.Parse(reader["DrugId"].ToString() ?? throw new BaseSQLiteRepositoryException("ReadDrugsIdsAsync")));
                }
                await _dbConnection.CloseAsync();
            }
        }
        return drugsIs;
    }

    public override async Task<TreatmentStage> ReadAsync(int id)
    {
        var drugsId = await ReadIdsAsync(id, "DrugId", "TreatmentStageDrug");
        var refsId = await ReadIdsAsync(id, "ReferralForAnalysisId", "TreatmentStageReferralForAnalysis");

        return new TreatmentStage(
            await DrugRepository.ReadAsync(drugsId),
            await ReferralForAnalysisRepository.ReadAsync(refsId),
        )

        throw new NotImplementedException();
    }

    public Task UpdateAsync(TreatmentStage nextEntity)
    {
        throw new NotImplementedException();
    }
}

public class TreatmentCourseRepositrory : BaseSQLiteRepository<TreatmentCourse>, ITreatmentCourseRepositrory
{
    public TreatmentCourseRepositrory(string connectionString, string tableName, IReferralForAnalysisRepository referralForAnalysisRepository) : base(connectionString, tableName)
    {
        ReferralForAnalysisRepository = referralForAnalysisRepository;
    }

    public IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }

    public ITreatmentStageRepository TreatmentStageRepository => throw new NotImplementedException();

    public Task AddAsync(TreatmentCourse entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TreatmentCourse oldEntity)
    {
        throw new NotImplementedException();
    }

    public override Task<TreatmentCourse?> ReadAsync(int id)
    {
        var TreatmentPartsIds = ReadPremitiveArrayFromColumnAsync<int>("TreatmentPartId", id);

        // return new TreatmentCourse()
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TreatmentCourse nextEntity)
    {
        throw new NotImplementedException();
    }
}