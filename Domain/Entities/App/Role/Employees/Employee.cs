using Domain.Entities.Common;
using Domain.Entities.People.Attribute;

namespace Domain.Entities.App.Role.Employees;

public class Employee : User
{
    int JobTittleId { get; }
    public Employee(string login,
                    string password,
                    string name,
                    string surname,
                    string patronymicName,
                    Address address,
                    DateTime dateOfBirth,
                    Sex sex,
                    int id,
                    MedicalPolicy policy,
                    ICollection<Contact> contacts,
                    ICollection<Education>? education,
                    decimal salaryPerHour,
                    ICollection<Benefit>? benefits,
                    string description) : base(login,
                                               password,
                                               name,
                                               surname,
                                               patronymicName,
                                               address,
                                               dateOfBirth,
                                               sex,
                                               id,
                                               policy,
                                               contacts,
                                               education,
                                               benefits)
    {
        SalaryPerHour = salaryPerHour;
        _description = description;
    }
    public string _description;
    public string Description { get => new string(_description); }
    public IEnumerable<string> WorkExperiencePlaces { get; }
    public int WorkExpirienceYearsOtherPlaces { get; }
    public decimal SalaryPerHour { get; set; }

    public int WorkedHoursThisMonth { get; private set; }

    public void IncrementWorkedHours() { WorkedHoursThisMonth++; }
}