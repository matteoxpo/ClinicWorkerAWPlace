using System.Reactive.Linq;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    private AppointmentRepository(string path) : base(path) { }

    private static AppointmentRepository? _globalRepositoryInstance;

    public static AppointmentRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new AppointmentRepository(
            "../../../../Data/DataSets/Appointment.json");
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

    public IEnumerable<Appointment> ReadByDoctor(Doctor doctor)
    {
        return ReadByDoctor(doctor.Login);
    }

    public IEnumerable<Appointment> ReadByDoctor(string doctorLogin)
    {
        return Read().Where(appointment => doctorLogin.Equals(appointment.DoctorLogin));
    }

    public IObservable<IEnumerable<Appointment>> ObserveByDoctor(string login)
    {
        return AsObservable.Select(d => d.Where(h => h.DoctorLogin.Equals(login)));
    }

    public IObservable<IEnumerable<Appointment>> ObserveByDoctor(Doctor doctor)
    {
        return ObserveByDoctor(doctor.Login);
    }
}