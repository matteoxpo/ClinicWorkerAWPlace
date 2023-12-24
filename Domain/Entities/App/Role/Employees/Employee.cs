using Domain.Entities.Common;
using Domain.Entities.People.Attribute;

namespace Domain.Entities.App.Role.Employees;

public class Employee : User
{
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
                    DateTime dateOfEmployment,
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
        DateOfEmployment = dateOfEmployment;
    }
    public string _description;
    public string Description { get => new string(_description); }
    public DateTime DateOfEmployment { get; }
    public IEnumerable<string> WorkExperiencePlaces { get; }
    public int WorkExpirienceYearsOtherPlaces { get; }
    public int GetWorkExpirienceYears()
    {
        var currentDate = DateTime.Now;
        int yearsWorked = currentDate.Year - DateOfEmployment.Year;

        if (currentDate.Month < DateOfEmployment.Month || (currentDate.Month == DateOfEmployment.Month && currentDate.Day < DateOfEmployment.Day))
        {
            yearsWorked--;
        }
        return yearsWorked;
    }
    public decimal SalaryPerHour { get; set; }

    public int WorkedHoursThisMonth { get; private set; }

    public void IncrementWorkedHours() { WorkedHoursThisMonth++; }
}