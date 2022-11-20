using Data.Models;
using Data.Db.HumanDB;
using Domain.Entities.People;

namespace Data.Db.HumanDB.ConnectWithDoctorEmployeeDB;

public class ConnectWithDoctorEmployeeDB : BaseConnectionWithDB<DoctorEmployee>
{
    private ConnectWithDoctorEmployeeDB(string pathToFile) : base(pathToFile) { }

    private static ConnectWithDoctorEmployeeDB? globalInstanceEmployeeDs;

    public static ConnectWithDoctorEmployeeDB GetInstance()
    { 
        return globalInstanceEmployeeDs ??= new ConnectWithDoctorEmployeeDB(
            "EmployeeDataStore.xml");
    }
    
}