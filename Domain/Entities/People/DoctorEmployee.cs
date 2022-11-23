using System.Collections.Generic;
namespace Domain.Entities.People;

public class DoctorEmployee : User
{
    public Qualifications Category { get; set; }
    public IEnumerable<Client> Patients { get; set; }

    public IEnumerable<string> Speciality { get; set; }



    public DoctorEmployee() : base("name", "surname", "password", "login")
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Patients = new List<Client>();
    }
    public DoctorEmployee(string name, string surname, string password, string login, Qualifications category, List<string> speciality, List<Client> clients)
    : base(name, surname, password, login)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Patients = new List<Client>(clients);
    }
}