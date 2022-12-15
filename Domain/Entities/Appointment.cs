namespace Domain.Entities;

public class Appointment
{
    public string ClientId { get; set; }
    public string DoctorLogin { get; set; }
    public DateTime MeetTime{ get; set; }

    public Appointment(string doctorLogin, string clientId, DateTime meetTime)
    {
        DoctorLogin = new string(doctorLogin);
        ClientId = new string(clientId);
        MeetTime = meetTime;
    }
    
    public Appointment()
    {
        DoctorLogin = new string("doctorLogin");
        ClientId = new string("clientPassport");
        MeetTime = new DateTime(0);
    }
    
}