using Domain.Common;
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
                    uint id,
                    MedicalPolicy policy,
                    ICollection<Contact> contacts,
                    ICollection<Education>? education,
                    decimal salaryPerHour,
                    DateTime dateOfEmployment,
                    string[]? workExperiencePlaces,
                    int workExpirienceYearsOtherPlaces,
                    ICollection<Benefit>? benefits) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        SalaryPerHour = salaryPerHour;
        DateOfEmployment = dateOfEmployment;
        WorkExperiencePlaces = workExperiencePlaces;
        WorkExpirienceYearsOtherPlaces = workExpirienceYearsOtherPlaces;
    }
    public DateTime DateOfEmployment { get; }
    public string[]? WorkExperiencePlaces { get; }
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