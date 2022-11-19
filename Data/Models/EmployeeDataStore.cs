using Domain.Common.People;
using Domain.Common;
using System.Collections.Generic;
namespace Data.Models;

[Serializable]
public class EmployeeDataStore
{
    public string Name;
    public string Surname;
    public string Login;
    public string Password;
    public Qualifications Category;
    public List<string> Speciality;
    public uint Id;

    public EmployeeDataStore(Employee p, uint id)
    {
        //  Data from original Employee
        Name = p.Name;
        Surname = p.Surname;
        Login = p.Login;
        Password = p.Password;
        Category = p.Category;
        Speciality = new List<string>(p.Speciality);
        // Service data for DataBase
        Id = id;
    }

    // public EmployeeDataStore(string name, string surname, string login, string password, , uint id)
    // {
    //     //  Data from original Employee
    //     Name = p.Name;
    //     Surname = p.Surname;
    //     Login = p.Login;
    //     Password = p.Password;
    //     Category = p.Category;
    //     Speciality = new List<string>(p.Speciality);
    //     // Service data for DataBase
    //     Id = id;
    // }
    public EmployeeDataStore()
    {
        Name = new string("name");
        Surname = new string("surname");
        Login = new string("login");
        Password = new string("password");
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Id = 0;
    }

    private Employee MapFromEmplyeeDS()
    {
        Employee mapped = new Employee(Name, Surname, Password, Login, Category, Speciality);
        return mapped;
    }



}