using Domain.Common;

namespace Domain.Entities.Polyclinic.Building;

public sealed class Polyclinic
{
    public Address Address { get; }

    public Polyclinic(Address address, ICollection<Cabinet> cabinets, IDictionary<DayOfWeek, (TimeSpan, TimeSpan)> schedule, uint iD)
    {
        Address = address ?? throw new NullReferenceException("Addres is null");
        Cabinets = cabinets ?? throw new NullReferenceException("CAbinets is null");
        Schedule = schedule ?? throw new NullReferenceException("Schedule is null");
        ID = iD;
    }

    public ICollection<Cabinet> Cabinets { get; }
    public IDictionary<DayOfWeek, (TimeSpan, TimeSpan)> Schedule { get; set; }
    public uint ID { get; }

}