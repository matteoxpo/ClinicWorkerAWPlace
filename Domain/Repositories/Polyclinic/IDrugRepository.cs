using Domain.Entities.Polyclinic.Drug;

namespace Domain.Repositories.Polyclinic;

public interface IDrugRepository : IReadaleAll<Drug>, IAddable<Drug>
{
}