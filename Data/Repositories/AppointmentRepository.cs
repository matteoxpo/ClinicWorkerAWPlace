using System.Reactive.Linq;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(string path) : base(path)
    {
    }

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

    public override bool CompareEntities(Appointment changedEntity, Appointment entity)
    {
        return changedEntity.ClientId.Equals(entity.ClientId) &&
               changedEntity.DoctorLogin.Equals(entity.DoctorLogin) &&
               changedEntity.MeetTime.Equals(entity.MeetTime);
    }

    public IEnumerable<Appointment> ReadByClient(Client client)
    {
        return ReadByClient(client.Id);
    }

    public IEnumerable<Appointment> ReadByClient(string passportSerial)
    {
        var clientAppoinmetns = new List<Appointment>();
        foreach (var appointment in Read())
        {
            if (appointment.ClientId.Equals(passportSerial))
            {
                clientAppoinmetns.Add(appointment);
            }
        }

        return clientAppoinmetns;
    }

    public IEnumerable<Appointment> ReadByDoctor(Doctor doctor)
    {
        return ReadByDoctor(doctor.Login);
    }

    public IEnumerable<Appointment> ReadByDoctor(string doctorLogin)
    {
        var doctorAppoinmetns = new List<Appointment>();
        foreach (var appointment in Read())
        {
            if (doctorLogin.Equals(appointment.DoctorLogin))
            {
                doctorAppoinmetns.Add(appointment);
            }
        }

        return doctorAppoinmetns;
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