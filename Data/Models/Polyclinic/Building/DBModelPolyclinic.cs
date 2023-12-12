using Data.Models.Common;

namespace Data.Models.Polyclinic.Building;

public sealed class DBModelPolyclinic
{
    public DBModelAddress Address { get; }

    public DBModelPolyclinic(DBModelAddress address, ICollection<DBModelCabinet> cabinets, IDictionary<DayOfWeek, (TimeSpan, TimeSpan)> schedule, uint iD)
    {
        Address = address ?? throw new NullReferenceException("Addres is null");
        Cabinets = cabinets ?? throw new NullReferenceException("CAbinets is null");
        Schedule = schedule ?? throw new NullReferenceException("Schedule is null");
        ID = iD;
    }

    public ICollection<DBModelCabinet> Cabinets { get; }
    public IDictionary<DayOfWeek, (TimeSpan, TimeSpan)> Schedule { get; set; }
    public uint ID { get; }

}