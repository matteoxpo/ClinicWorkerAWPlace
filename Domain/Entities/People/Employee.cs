using System.Collections.Generic;
namespace Domain.Entities.People;
public class Employee : User
{
    public Qualifications Category { get; set; }
    public IEnumerable<string> Speciality { get; set; }
    public IEnumerable<Client> Patients { get; set; }



    public Employee() : base("name", "surname", "password", "login")
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Patients = new List<Client>();
    }
    public Employee(string name, string surname, string password, string login, Qualifications category, List<string> speciality)
    : base(name, surname, password, login)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Patients = new List<Client>();
    }
}