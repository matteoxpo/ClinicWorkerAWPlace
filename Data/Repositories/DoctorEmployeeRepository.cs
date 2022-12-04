using System.Net;
using System.Reactive.Linq;
using Domain.Entities.People;
using Domain.Repositories;

namespace Data.Repositories;
public class DoctorEmployeeRepository : BaseRepository<DoctorEmployee>, IDoctorEmployeeRepository
{
    // AutoFAC
    
    // make private
    private DoctorEmployeeRepository(string pathToFile) : base(pathToFile) { }

    
    private static DoctorEmployeeRepository? _globalRepositoryInstance;
  

    public static DoctorEmployeeRepository GetInstance()
    { 
        return _globalRepositoryInstance ??= new DoctorEmployeeRepository(
            "../../../../Data/DataSets/Doctors.json");
    }
    

    protected override bool CompareEntities(DoctorEmployee entity1, DoctorEmployee entity2)
    {
        return string.Equals(entity1.Login, entity2.Login);
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

    public IEnumerable<DoctorEmployee> Read()
    {
        return DeserializationJson(); 
    }

    public IObservable<DoctorEmployee> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => emp.Login.Equals(login));
            }
        )!.Where<DoctorEmployee>((d) => d is not null);
    }
   
}