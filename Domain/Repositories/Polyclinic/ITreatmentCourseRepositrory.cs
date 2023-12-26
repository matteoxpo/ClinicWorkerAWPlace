using Domain.Entities.Polyclinic.Treatment;

namespace Domain.Repositories.Polyclinic;
public interface ITreatmentStageRepository : IUpdatable<TreatmentStage>, IAddable<TreatmentStage>, IReadale<TreatmentStage>
{
    IDrugRepository DrugRepository { get; }
    IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }
    IDiseaseRepository DiseaseRepository { get; }
}

public interface ITreatmentCourseRepositrory : IBaseRepository<TreatmentCourse>
{
    IReferralForAnalysisRepository ReferralForAnalysisRepository { get; }
    ITreatmentStageRepository TreatmentStageRepository { get; }
}
