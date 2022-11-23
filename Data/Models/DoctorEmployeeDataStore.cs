using Domain.Entities.People;
using Domain.Entities;
using System.Collections.Generic;
namespace Data.Models;

[Serializable]
public class DoctorEmployeeDataStore
{
    public string Name;
    public string Surname;
    public string Login;
    public string Password;
    public Qualifications Category;
    public List<string> Speciality;
    public List<Client> Patients;
    public int Id;

    public DoctorEmployeeDataStore(DoctorEmployee p, int id)
    {
        //  Data from original DoctorEmployee
        Name = new string(p.Name);
        Surname = new string(p.Surname);
        Login = new string(p.Login);
        Password = new string(p.Password);
        Category = p.Category;
        Speciality = new List<string>(p.Speciality);
        Patients = new List<Client>(p.Patients);
        // Service data for DataBase
        Id = id;
    }
    
    public DoctorEmployeeDataStore()
    {
        Name = new string("name");
        Surname = new string("surname");
        Login = new string("login");
        Password = new string("password");
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Patients = new List<Client>();
        Id = 0;
    }

    public DoctorEmployee MapToDoctorEmployee()
    {
        return new DoctorEmployee( Name, Surname, Password, Login, Category, Speciality, Patients);
    }





}