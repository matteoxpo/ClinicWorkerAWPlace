using System.Reactive.Linq;
using Data.Models.People;
using Domain.Entities.People;
using Domain.Entities.Role;
using Domain.Repositories;

namespace Data.Repositories;

public class UserEmployeeRepository : BaseRepository<UserEmployee, UserEmployeeStorageModel>, IUserEmployeeRepository
{
    private static UserEmployeeRepository? _globalRepositoryInstance;

    private readonly IAdminRepository _adminRepository;
    private readonly IDoctorRepository _doctorRepository;

    private UserEmployeeRepository(string pathToFile, IAdminRepository adminRepository,
        IDoctorRepository doctorRepository) : base(pathToFile)
    {
        _adminRepository = adminRepository;
        _doctorRepository = doctorRepository;
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

    public void Create(UserEmployee newUserEmployee)
    {
        Append(newUserEmployee);
    }

    public IEnumerable<UserEmployee> ReadOnlyLoginPassword()
    {
        return DeserializationJson();
    }

    public IEnumerable<UserEmployee> Read()
    {
        var employees = new List<UserEmployee>(DeserializationJson());
        var userEmployees = new List<UserEmployee>();

        foreach (var employee in employees)
        {
            var jobs = new List<UserRole>();
            jobs.AddRange(_doctorRepository.Read().Where(d => d.Login.Equals(employee.Login)));
            jobs.AddRange(_adminRepository.Read().Where(d => d.Login.Equals(employee.Login)));
            userEmployees.Add(new UserEmployee(
                employee.Name,
                employee.Surname,
                employee.Login,
                employee.Password,
                jobs,
                employee.DateOfBirth)
            );
        }


        return userEmployees;
    }

    public IObservable<UserEmployee> ObserveByLogin(string login)
    {
        return AsObservable.Select(
            empl => { return empl.FirstOrDefault(emp => emp.Login.Equals(login)); }
        )!.Where<UserEmployee>(d => true);
    }

    public static UserEmployeeRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new UserEmployeeRepository(
            "../../../../Data/DataSets/UserEmployee.json", AdminRepository.GetInstance(),
            DoctorRepository.GetInstance());
    }
}