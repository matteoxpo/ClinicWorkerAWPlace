using Data.Models;
using Data.Db.HumanDB;
using Domain.Entities.People;

namespace Data.Db.HumanDB.EmployeeDB;

public class ConnectWithEmployeeDS : BaseConnectionWithDB<EmployeeDataStore>
{
    // private List<EmployeeDataStore> Data;
    public ConnectWithEmployeeDS(string pathToFile) : base(pathToFile) { }

    // public bool IsUserLoginPasswordExist(string login, string password)
    // {
    //     int loginIndex = _entity.FindIndex(e => string.Equals(e.Login, login));
    //     bool res = false;
    //     if (loginIndex != -1)
    //         res = string.Equals(_entity[loginIndex].Password, password);
    //     return ();
    // }



}