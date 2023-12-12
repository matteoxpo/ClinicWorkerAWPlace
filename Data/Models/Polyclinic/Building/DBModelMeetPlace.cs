
namespace Data.Models.Polyclinic.Building;


public sealed class DBModelMeetPlace
{
    public DBModelCabinet Cabinet { get; }
    public DBModelPolyclinic Polyclinic { get; }
    public DBModelMeetPlace(DBModelPolyclinic polyclinic, DBModelCabinet cabinet)
    {
        if (!polyclinic.Cabinets.Contains(cabinet))
        {
            throw new MeetPlaceException($"Cabinet [Number:{cabinet.Number} ID:{cabinet.ID} Descriotion:{cabinet.Description}] is not in polyclininc:[Address{polyclinic.Address} ID:{polyclinic.ID}]");
        }
        Cabinet = cabinet;
        Polyclinic = polyclinic;
    }
    public DBModelMeetPlace(DBModelPolyclinic polyclinic, string cabinetNumber)
    {
        Polyclinic = polyclinic;
        foreach (var cabinet in polyclinic.Cabinets)
        {
            if (string.Equals(cabinet.Number, cabinetNumber))
            {
                Cabinet = cabinet;
                return;
            }
        }
        throw new MeetPlaceException($"Cant find cabinet with number {cabinetNumber} in polyclininc:[Address{polyclinic.Address} ID:{polyclinic.ID}]");
    }
    public DBModelMeetPlace(DBModelPolyclinic polyclinic, uint cabinetID)
    {
        Polyclinic = polyclinic;
        foreach (var cabinet in polyclinic.Cabinets)
        {
            if (cabinet.ID == cabinetID)
            {
                Cabinet = cabinet;
                return;
            }
        }

        throw new MeetPlaceException($"Cant find cabinet with ID {cabinetID} in polyclininc:[Address{polyclinic.Address} ID:{polyclinic.ID}]");
    }

}

public class MeetPlaceException : Exception
{
    public MeetPlaceException(string message) : base(message) { }
}