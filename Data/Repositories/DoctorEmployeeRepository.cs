using System.Reactive.Linq;
using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Data.Models;
using System.Text.RegularExpressions;
using Data.Db.HumanDB.ConnectWithDoctorEmployeeDB;
namespace Data.Repositories;

public class DoctorEmployeeRepository : IDoctorEmployeeRepository
{
    private ConnectWithDoctorEmployeeDB _connection;


    public DoctorEmployeeRepository()
    {
        _connection =  ConnectWithDoctorEmployeeDB.GetInstance();
    }
    
    
    public void UpdateDoctorEmployee(DoctorEmployee newDoctorEmployee)
    {
        throw new NotImplementedException();
    }

    public IObservable<DoctorEmployee> ObserveByLogin(string login)
    {
       return _connection.AsObservable.Select(
            (empl) =>
            {
                return empl.FirstOrDefault((emp) => string.Equals(emp.Login, login));
            }
            )!.Where<DoctorEmployee>((d) => !d.Equals(null));
    }
}

