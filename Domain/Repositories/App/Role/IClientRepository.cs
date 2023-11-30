using Domain.Entities.App.Role;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Repositories.App.Role;

public interface IClientRepository<ID> : IUserRepository<Client, ID>
{
    public ICollection<Appointment> GetAppointments(ID id);
    public ICollection<Appointment> GetAppointments(string login);
    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(string login);
    public ICollection<ReferralForAnalysis> GetReferralForAnalyses(ID id);
}