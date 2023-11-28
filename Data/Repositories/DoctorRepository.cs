using System.Reactive.Linq;
using Data.Models.Roles;
using Domain.Entities.Role;
using Domain.Entities.Role.Doctor;
using Domain.Repositories;

namespace Data.Repositories;

public class DoctorRepository : BaseRepository<Doctor, DoctorStorageModel>, IDoctorRepository
{
    private static DoctorRepository? _globalRepositoryInstance;
    private readonly IAppointmentRepository _appointmentRepository;

    private DoctorRepository(string path, IAppointmentRepository appointmentRepository) : base(path)
    {
        _appointmentRepository = appointmentRepository;
    }


    public void Update(Doctor nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(Doctor oldEntity)
    {
        Remove(oldEntity);
    }

    public void Create(Doctor newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<Doctor> Read()
    {
        return DeserializationJson()
            .Select(doctor => new Doctor(
                doctor.Category,
                doctor.Speciality,
                _appointmentRepository.ReadByDoctor(doctor),
                doctor.Login,
                doctor.ID)
            );
    }

    public override bool CompareEntities(Doctor entity1, Doctor entity2)
    {
        return entity1.Login.Equals(entity2.Login);
    }

    public IObservable<Doctor> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            empl => { return empl.FirstOrDefault(emp => emp.Login.Equals(login)); }
        )!.Where<Doctor>(d => d is not null);
    }

    public static DoctorRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new DoctorRepository(
            "../../../../Data/DataSets/Doctor.json", AppointmentRepository.GetInstance());
    }
}