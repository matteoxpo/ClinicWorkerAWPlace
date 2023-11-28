using Domain.Entities.Polyclinic.Treatment;

namespace Domain.Repositories.Polyclinic;

public interface ITreatmentCourseRepositrory<ID> : IBaseRepository<TreatmentCourse, ID>
{
    IReferralForAnalysisRepository<ID> _referralForAnalysisRepository { get; }
}