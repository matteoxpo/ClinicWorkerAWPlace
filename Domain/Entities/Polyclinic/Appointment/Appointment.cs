using Domain.Entities.Polyclinic.Building;

namespace Domain.Entities.Polyclinic.Appointment;

public sealed class Appointment
{
    public uint? FromDoctorID { get; } = null;
    public uint DoctorID { get; }
    public uint ClientID { get; }
    public DateTime Date { get; set; }

    public string? Description { get; set; }
    public MeetPlace MeetPlace { get; set; }
    public uint ID { get; }


    public Appointment(uint doctorID, uint clientID, DateTime date, uint appointmentID, MeetPlace meetPlace, uint? fromDoctorID = null)
    {
        DoctorID = doctorID;
        ClientID = clientID;
        Date = date;
        ID = appointmentID;

        FromDoctorID = fromDoctorID;
        MeetPlace = meetPlace;
    }

}