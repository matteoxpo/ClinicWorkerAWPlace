namespace Domain.Repositories.Polyclinic;

using Domain.Entities.Polyclinic.Building;
using Domain.Repositories.Common;

public interface ICabinetRepository : IReadaleAll<Cabinet>
{
    Task<IEnumerable<Cabinet>> ReadCabinetByClinicIdAsync(int id);
    IEnumerable<Cabinet> ReadCabinetByClinicId(int id)
    {
        return ReadCabinetByClinicIdAsync(id).GetAwaiter().GetResult();
    }

}

public interface IMedicineClinicRepository : IReadaleAll<MedicineClinic>
{
    ICabinetRepository CabinetRepository { get; }
    IContactRepository ContactRepository { get; }
    IAddressRepository AddressRepository { get; }
}