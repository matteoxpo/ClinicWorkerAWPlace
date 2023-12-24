using Domain.Entities.Polyclinic.Building;

namespace Domain.Entities.Polyclinic.Appointment;

public sealed class Appointment
{
    public int? FromDoctorID { get; } = null;
    public int DoctorID { get; }
    public int ClientID { get; }
    public DateTime Date { get; set; }

    public string? Description { get; set; }
    public MeetPlace MeetPlace { get; set; }
    public int ID { get; }


    public Appointment(int doctorID, int clientID, DateTime date, int appointmentID, MeetPlace meetPlace, int? fromDoctorID = null)
    {
        DoctorID = doctorID;
        ClientID = clientID;
        Date = date;
        ID = appointmentID;

        FromDoctorID = fromDoctorID;
        MeetPlace = meetPlace;
    }

}