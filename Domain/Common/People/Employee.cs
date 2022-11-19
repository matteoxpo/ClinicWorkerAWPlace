using System.Collections.Generic;
namespace Domain.Common.People;
public class Employee : User
{
    public Qualifications Category { get => _category; }
    public List<string> Speciality { get => _speciality; }

    private List<string> _speciality { get; set; }

	

    public List<Client> Patients;
    

	protected Qualifications _category { get; set; }


    public Employee() : base("name", "surname", "password", "login")
    {
        _category = Qualifications.FirstCategory;
        _speciality = new List<string>();
        Patients = new List<Client>();
    }
    public Employee(string name, string surname, string password, string login, Qualifications category, List<string> speciality)
    : base(name, surname, password, login)
    {
        _category = category;
        _speciality = new(speciality);
        Patients = new List<Client>();
    }
}