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
    public List<ClientDataStore> Patients;
    public int Id;

    public DoctorEmployeeDataStore(DoctorEmployee p, IEnumerable<ClientDataStore> patients, int id)
    {
        Name = new string(p.Name);
        Surname = new string(p.Surname);
        Login = new string(p.Login);
        Password = new string(p.Password);
        Category = p.Category;
        Speciality = new List<string>(p.Speciality);
        Patients = new List<ClientDataStore>(patients);
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
        Patients = new List<ClientDataStore>();
        Id = 0;
    }

    public DoctorEmployee MapToDoctorEmployee()
    {
        var patients = new List<Client>();
        foreach (var pat in Patients)
        {
            patients.Add(pat.MapToClent());
        }
        
        return new DoctorEmployee( Name, Surname, Password, Login, Category, Speciality, patients);
    }





}