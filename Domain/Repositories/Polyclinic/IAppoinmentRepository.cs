using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Repositories.Polyclinic;

public interface IAppoinmentRepository : IBaseRepository<Appointment>
{
    IMedicineClinicRepository MedicineClinicRepository { get; }
    ICabinetRepository CabinetRepository { get; }
}