using Domain.Entities.Common;

namespace Domain.Entities.Polyclinic.Building;

public sealed class MedicineClinic
{
    public Address Address { get; }

    public MedicineClinic(Address address, IEnumerable<Cabinet> cabinets, Contact contact, int id)
    {
        Address = address ?? throw new NullReferenceException("Addres is null");
        Cabinets = cabinets ?? throw new NullReferenceException("CAbinets is null");
        Contact = contact;
        ID = id;

    }

    public IEnumerable<Cabinet> Cabinets { get; }
    public Contact Contact { get; }
    public int ID { get; }

}