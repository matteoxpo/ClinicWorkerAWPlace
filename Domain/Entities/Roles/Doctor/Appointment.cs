namespace Domain.Entities.Roles.Doctor;

public class Appointment
{
    public Appointment(string doctorLogin, string clientId, DateTime meetTime, string clientComplaints)
    {
        DoctorLogin = new string(doctorLogin);
        ClientId = new string(clientId);
        MeetTime = meetTime;
        ClientComplaints = new string(clientComplaints);
    }

    public string ClientId { get; }
    public string DoctorLogin { get; }
    public DateTime MeetTime { get; }
    public string ClientComplaints { get; }

    public override string ToString()
    {
        return string.Join(" ", DoctorLogin, MeetTime.ToString("HH:mm dd.MM.yyyy"));
    }
}