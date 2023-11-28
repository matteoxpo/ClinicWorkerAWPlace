using Domain.Entities.Polyclinic.Disease;

namespace Domain.Repositories.Polyclinic;

public interface IDeseaseRepository<ID> : IReadaleAll<Disease, ID>
{
}