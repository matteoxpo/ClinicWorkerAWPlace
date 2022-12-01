using System.Collections;
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
    public DateTime DateOfBirth;
    public Qualifications Category;
    public List<string> Speciality;
    public List<Tuple<ClientDataStore, DateTime>> Patients;
    public int Id;

    public DoctorEmployeeDataStore(DoctorEmployee p, IEnumerable<Tuple<ClientDataStore, DateTime>> patients, int id)
    {
        Name = new string(p.Name);
        Surname = new string(p.Surname);
        DateOfBirth = p.DateOfBirth;
        Login = new string(p.Login);
        Password = new string(p.Password);
        Category = p.Category;
        Speciality = new List<string>(p.Speciality);
        Patients = new List<Tuple<ClientDataStore, DateTime>>(patients);
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
        Patients = new List<Tuple<ClientDataStore, DateTime>>();
        Id = -1;
    }

    public DoctorEmployee MapToDoctorEmployee()
    {
        var patients = new List<Tuple<Client, DateTime>>();
        foreach (var patient in Patients)
        {
            patients.Add(new Tuple<Client, DateTime>(patient.Item1.MapToClent(), patient.Item2));
        }
        return new DoctorEmployee(Name, Surname, DateOfBirth, Login, Password, Category, Speciality, patients);
    }





}