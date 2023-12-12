using Data.Models.Polyclinic.Building;

namespace Data.Models.Polyclinic.Appointment;

public sealed class DBModelAppointment
{
    public uint? FromDoctorID { get; } = null;
    public uint DoctorID { get; }
    public uint ClientID { get; }
    public DateTime Date { get; set; }

    public string? Description { get; set; }
    public DBModelMeetPlace MeetPlace { get; set; }
    public uint ID { get; }


    public DBModelAppointment(uint doctorID, uint clientID, DateTime date, uint appointmentID, DBModelMeetPlace meetPlace, uint? fromDoctorID = null)
    {
        DoctorID = doctorID;
        ClientID = clientID;
        Date = date;
        ID = appointmentID;

        FromDoctorID = fromDoctorID;
        MeetPlace = meetPlace;
    }

}