using Data.Models;
using Domain.Entities.Roles.Doctor;

namespace Domain.Entities;

public class AppointmentStorageModel : IConverter<Appointment, AppointmentStorageModel>
{
    public AppointmentStorageModel(string doctorLogin, string clientId, DateTime meetTime, string clientComplaints)
    {
        DoctorLogin = new string(doctorLogin);
        ClientId = new string(clientId);
        MeetTime = meetTime;
        ClientComplaints = new string(clientComplaints);
    }

    public AppointmentStorageModel()
    {
        DoctorLogin = new string("doctorLogin");
        ClientId = new string("clientPassport");
        MeetTime = new DateTime(0);
        ClientComplaints = new string("ClientComplaints");
    }

    public string ClientId { get; set; }
    public string DoctorLogin { get; set; }
    public DateTime MeetTime { get; set; }
    public string ClientComplaints { get; set; }

    public Appointment ConvertToEntity(AppointmentStorageModel entity)
    {
        return new Appointment(entity.DoctorLogin, entity.ClientId, entity.MeetTime, entity.ClientComplaints);
    }

    public AppointmentStorageModel ConvertToStorageEntity(Appointment entity)
    {
        return new AppointmentStorageModel(entity.DoctorLogin, entity.ClientId, entity.MeetTime,
            entity.ClientComplaints);
    }

    public override string ToString()
    {
        return string.Join(" ", DoctorLogin, MeetTime.ToString("HH:mm dd.MM.yyyy"));
    }
}