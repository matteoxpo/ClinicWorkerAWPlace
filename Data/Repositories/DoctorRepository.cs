using System.Reactive.Linq;
using System.Text;
using Domain.Entities;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    private IAppointmentRepository _appointmentRepository;
    
    private DoctorRepository(string path, IAppointmentRepository appointmentRepository) : base(path)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    private static DoctorRepository? _globalRepositoryInstance;
    public static DoctorRepository GetInstance()
    { 
        return _globalRepositoryInstance ??= new DoctorRepository(
            "../../../../Data/DataSets/Doctor.json", AppointmentRepository.GetInstance());
    }


    public void Update(Doctor nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(Doctor oldEntity)
    {
        Remove(oldEntity);
    }

    public void Add(Doctor newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<Doctor> Read()
    {
        var doctors =  DeserializationJson();
        foreach (var doctor in doctors)
        {
            doctor.Appointments = _appointmentRepository.ReadByDoctor(doctor);
        }

        return doctors;
    }

    public override bool CompareEntities(Doctor changedEntity, Doctor entity)
    {
        return changedEntity.Login.Equals(entity.Login);
    }

    public IObservable<Doctor> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => emp.Login.Equals(login));
            }
        )!.Where<Doctor>((d) => d is not null);
    }
}