using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Repositories.Polyclinic;

public interface IAppoinmentRepository<ID> : IBaseRepository<Appointment, ID> { }