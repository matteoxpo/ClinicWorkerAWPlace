using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using Domain.Entities.Polyclinic.Treatment;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.Polyclinic;

public class TreatmentStageRepository : BaseSQLiteRepository<TreatmentStage>, ITreatmentStageRepository
{
    public IDrugRepository DrugRepository { get; }

    public IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }
    public IDiseaseRepository DiseaseRepository { get; }


    public TreatmentStageRepository(SQLiteConnection dbConnection, string tableName, IDrugRepository drugRepository, IReferralForAnalysisRepository referralForAnalysisRepository, IDiseaseRepository diseaseRepository) : base(dbConnection, tableName)
    {
        DrugRepository = drugRepository;
        ReferralForAnalysisRepository = referralForAnalysisRepository;
        DiseaseRepository = diseaseRepository;
    }

    public async Task AddAsync(TreatmentStage entity)
    {
        await AddRowAsync(
            new Dictionary<string, string>() {
                {"EmployeeUserId",entity.DoctorId.ToString()},
                {"Description", entity.Description},
                {"TreatmentCourseId", entity.ID.ToString()}
            }
        );

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
    }
    private async Task<ICollection<int>> ReadIdsAsync(int stageId, string variable, string tableName)
    {
        var drugsIs = new List<int>();
        using (var command = new SQLiteCommand($"SELECT {variable} FROM {tableName} WHERE TreatmentStageId = @stageId", _dbConnection))
        {
            command.Parameters.AddWithValue("@stageId", stageId);
            using (var reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    drugsIs.Add(int.Parse(reader["DrugId"].ToString() ?? throw new BaseSQLiteRepositoryException("ReadDrugsIdsAsync")));
                }
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
            await ReadPremitiveAsync<string>("Description", id),
            await DiseaseRepository.ReadAsync(
                await ReadPremitiveAsync<int>("DiseaseId", id)
            ),
            await ReferralForAnalysisRepository.ReadAsync(refsId),
            id);
    }

    public Task UpdateAsync(TreatmentStage nextEntity)
    {
        throw new NotImplementedException();
    }
}

public class TreatmentCourseRepositrory : BaseSQLiteRepository<TreatmentCourse>, ITreatmentCourseRepositrory
{
    public TreatmentCourseRepositrory(SQLiteConnection dbConnection, string tableName, IReferralForAnalysisRepository referralForAnalysisRepository, ITreatmentStageRepository treatmentStageRepository) : base(dbConnection, tableName)
    {
        ReferralForAnalysisRepository = referralForAnalysisRepository;
        TreatmentStageRepository = treatmentStageRepository;
    }

    public IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }

    public ITreatmentStageRepository TreatmentStageRepository { get; }

    public async Task AddAsync(TreatmentCourse entity)
    {

        await TreatmentStageRepository.AddAsync(entity.TreatmentStages);
        await AddRowAsync(new Dictionary<string, string>()
        {

        });
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TreatmentCourse oldEntity)
    {
        throw new NotImplementedException();
    }

    public override async Task<TreatmentCourse?> ReadAsync(int id)
    {
        var stagesId = new List<int>();
        using (var command = new SQLiteCommand($"SELECT Id FROM TreatmentStage WHERE TreatmentCourseId = @treatmentCourseId", _dbConnection))
        {
            command.Parameters.AddWithValue("@treatmentCourseId", id); // Замените это на значение, которое вы используете для сравнения

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    stagesId.Add(int.Parse(reader["Id"].ToString()));
                }
            }
        }
        return new TreatmentCourse(await TreatmentStageRepository.ReadAsync(stagesId), id);
    }

    public Task UpdateAsync(TreatmentCourse nextEntity)
    {
        throw new NotImplementedException();
    }
}