using Domain.Entities.App.Role;
using Domain.Repositories.Polyclinic;

namespace Domain.Repositories.App.Role;

public interface IClientRepostory<ID> : IBaseRepository<Client, ID>
{
    IAppoinmentRepository<ID> _appoinmentRepository { get; }
    ITreatmentCourseRepositrory<ID> _treatmentCourseRepositrory { get; }
}