using System.Security.Cryptography.X509Certificates;

namespace Domain.Entities.Polyclinic.Building;


public class MeetPlace
{
    public Cabinet Cabinet { get; }
    public Polyclinic Polyclinic { get; }
    public MeetPlace(Polyclinic polyclinic, Cabinet cabinet)
    {
        if (!polyclinic.Cabinets.Contains(cabinet))
        {
            throw new MeetPlaceException($"Cabinet [Number:{cabinet.Number} ID:{cabinet.ID} Descriotion:{cabinet.Description}] is not in polyclininc:[Address{polyclinic.Address} ID:{polyclinic.ID}]");
        }
        Cabinet = cabinet;
        Polyclinic = polyclinic;
    }
    public MeetPlace(Polyclinic polyclinic, string cabinetNumber)
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
    public MeetPlace(Polyclinic polyclinic, uint cabinetID)
    {
        Polyclinic = polyclinic;
        foreach (var cabinet in polyclinic.Cabinets)
        {
            if (cabinet.ID == cabinetID))
            {
                Cabinet = cabinet;
            }
            return;
        }
        throw new MeetPlaceException($"Cant find cabinet with ID {cabinetID} in polyclininc:[Address{polyclinic.Address} ID:{polyclinic.ID}]");
    }

}

public class MeetPlaceException : Exception
{
    public MeetPlaceException(string message) : base(message) { }
}