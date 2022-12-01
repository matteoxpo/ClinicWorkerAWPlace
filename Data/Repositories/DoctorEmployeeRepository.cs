using System.Reactive.Linq;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;
public class DoctorEmployeeRepository : BaseRepository<DoctorEmployee>, IDoctorEmployeeRepository
{
    // AutoFAC
    
    // make private
    private DoctorEmployeeRepository(string pathToFile) : base(pathToFile) { }

    private static DoctorEmployeeRepository? globalRepositoryInstance;

    public static DoctorEmployeeRepository GetInstance()
    { 
        return globalRepositoryInstance ??= new DoctorEmployeeRepository(
            "../../../../Data/DataSets/Doctors.json");
    }
    

    protected override bool CompareEntities(DoctorEmployee changedEntity, DoctorEmployee entity)
    {
        return string.Equals(changedEntity.Login, entity.Login);
    }

    public void Update(DoctorEmployee newDoctorEmployee)
    {
        Change(newDoctorEmployee);
    }

    public void Delete(DoctorEmployee newDoctorEmployee)
    {
        Remove(newDoctorEmployee);
    }

    public void Add(DoctorEmployee newDoctorEmployee)
    {
        Append(newDoctorEmployee);
    }

    public List<DoctorEmployee> Read()
    {
        return DeserializationJson(); 
    }

    public IObservable<DoctorEmployee> ObserveById(int id)
    {
        throw new NotImplementedException();
    }

    public IObservable<DoctorEmployee> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => emp.Name.Equals(login));
            }
        )!.Where<DoctorEmployee>((d) => !d.Equals(null));
    }
}