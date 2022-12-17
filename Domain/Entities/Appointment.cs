namespace Domain.Entities;

public class Appointment
{
    public string ClientId { get; set; }
    public string DoctorLogin { get; set; }
    public DateTime MeetTime{ get; set; }
    public string ClientComplaints { get; set; }

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
    
}