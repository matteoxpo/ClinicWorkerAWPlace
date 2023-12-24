using Domain.Entities.Polyclinic.Analysis;

namespace Domain.Repositories.Polyclinic;

public interface IReferralForAnalysisRepository : IBaseRepository<ReferralForAnalysis>
{
    IAnalysisRepository AnalysisRepository { get; }
}