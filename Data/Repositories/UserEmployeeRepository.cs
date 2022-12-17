using System.Net;
using System.Reactive.Linq;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;
public class UserEmployeeRepository : BaseRepository<UserEmployee>, IUserEmployeeRepository
{
    private UserEmployeeRepository(string pathToFile, IAdminRepository adminRepository, IDoctorRepository doctorRepository) : base(pathToFile)
    {
        _adminRepository = adminRepository;
        _doctorRepository = doctorRepository;
    }

    
    private static UserEmployeeRepository? _globalRepositoryInstance;

    private IAdminRepository _adminRepository;
    private IDoctorRepository _doctorRepository;
    public static UserEmployeeRepository GetInstance()
    { 
        return _globalRepositoryInstance ??= new UserEmployeeRepository(
            "../../../../Data/DataSets/UserEmployee.json", AdminRepository.GetInstance(), DoctorRepository.GetInstance());
    }
    

    public override bool CompareEntities(UserEmployee entity1, UserEmployee entity2)
    {
        return string.Equals(entity1.Login, entity2.Login);
    }

    public void Update(UserEmployee changedUserEmployee)
    {
        Change(changedUserEmployee);
    }

    public void Delete(UserEmployee oldUserEmployee)
    {
        Remove(oldUserEmployee);
    }

    public void Add(UserEmployee newUserEmployee)
    {
        Append(newUserEmployee);
    }

    public IEnumerable<UserEmployee> ReadOnlyLoginPassword()
    {
        return DeserializationJson();
    }

    public IEnumerable<UserEmployee> Read()
    {
        var employees =  new List<UserEmployee>(DeserializationJson());
        var dJobs = new List<Doctor>(_doctorRepository.Read());
        var aJobs = new List<Admin>(_adminRepository.Read());

        foreach (var employee in employees)
        {
            var jobs = new List<JobTitle>();

            jobs.AddRange(dJobs.Where(d => d.Login.Equals(employee.Login)));
            jobs.AddRange(aJobs.Where(d => d.Login.Equals(employee.Login)));
            employee.JobTitles = jobs;
        }
        

        return employees;
    }

    public IObservable<UserEmployee> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => emp.Login.Equals(login));
            }
        )!.Where<UserEmployee>((d) => true);
    }
   
}