using Domain.Entities.Polyclinic.Disease;

namespace Domain.Repositories.Polyclinic;

public interface IDiseaseRepository : IReadaleAll<Disease>, IAddable<Disease>
{
}