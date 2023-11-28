namespace Domain.Repositories.Polyclinic;

using Domain.Entities.Polyclinic.Building;
public interface ICabinetRepository<ID> : IReadaleAll<Polyclinic, ID> { }

public interface IPolyclinicRepository<ID> : IReadaleAll<Polyclinic, ID>
{
    ICabinetRepository<ID> _cabinetRepository { get; }
}