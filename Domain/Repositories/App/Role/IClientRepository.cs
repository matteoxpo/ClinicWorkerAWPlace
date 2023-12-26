using Domain.Entities.App.Role;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Repositories.Polyclinic;

namespace Domain.Repositories.App.Role;

public interface IClientRepository : IUserRepository<Client>
{
    public ICollection<Appointment> GetAppointments(int id);
    public ICollection<Appointment> GetAppointments(string login);
    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(string login);
    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(int id);
    public IAppoinmentRepository AppoinmentRepository { get; }
    public ITreatmentCourseRepositrory TreatmentCourseRepositrory { get; }
}