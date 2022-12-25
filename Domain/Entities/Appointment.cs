namespace Domain.Entities;

public class Appointment
{
    public Appointment(string doctorLogin, string clientId, DateTime meetTime, string clientComplaints)
    {
        DoctorLogin = new string(doctorLogin);
        ClientId = new string(clientId);
        MeetTime = meetTime;
        ClientComplaints = new string(clientComplaints);
    }

    public Appointment()
    {
        DoctorLogin = new string("doctorLogin");
        ClientId = new string("clientPassport");
        MeetTime = new DateTime(0);
        ClientComplaints = new string("ClientComplaints");
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