using Data.Models.People;
using Data.Models.Roles;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class AppointmentRepository : BaseRepository<Appointment, AppointmentStorageModel>, IAppointmentRepository
{
    private static AppointmentRepository? _globalRepositoryInstance;

    private AppointmentRepository(string path) : base(path)
    {
    }

    public void Update(Appointment nextAppointment)
    {
        Change(nextAppointment);
    }

    public void Delete(Appointment oldAppointment)
    {
        Remove(oldAppointment);
    }

    public void Add(Appointment newAppointment)
    {
        Append(newAppointment);
    }


    public IEnumerable<Appointment> Read()
    {
        return DeserializationJson();
    }

    public override bool CompareEntities(Appointment entity1, Appointment entity2)
    {
        return entity1.ClientId.Equals(entity2.ClientId) &&
               entity1.DoctorLogin.Equals(entity2.DoctorLogin) &&
               entity1.MeetTime.Equals(entity2.MeetTime);
    }

    public IEnumerable<Appointment> ReadByClient(Client client)
    {
        return ReadByClient(client.Id);
    }

    public IEnumerable<Appointment> ReadByClient(string passportSerial)
    {
        return Read().Where(appointment => appointment.ClientId.Equals(passportSerial));
    }

    public IEnumerable<Appointment> ReadByDoctor(Doctor doctorStorageModel)
    {
        return ReadByDoctor(doctorStorageModel.Login);
    }

    public IEnumerable<Appointment> ReadByDoctor(string doctorLogin)
    {
        return Read().Where(appointment => doctorLogin.Equals(appointment.DoctorLogin));
    }

    public static AppointmentRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new AppointmentRepository(
            "../../../../Data/DataSets/Appointment.json");
    }
}