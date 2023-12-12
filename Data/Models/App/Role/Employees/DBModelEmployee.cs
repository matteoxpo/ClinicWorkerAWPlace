using Data.Models.Common;
using Data.Models.People.Attribute;
using Domain.Entities.People.Attribute;

namespace Data.Models.App.Role.Employees;

public class DBModelEmployee : DBModelUser
{
    public DBModelEmployee(string login,
                    string password,
                    string name,
                    string surname,
                    string patronymicName,
                    DBModelAddress address,
                    DateTime dateOfBirth,
                    Sex sex,
                    uint id,
                    DBModelMedicalPolicy policy,
                    ICollection<DBModelContact> contacts,
                    ICollection<DBModelEducation>? education,
                    decimal salaryPerHour,
                    DateTime dateOfEmployment,
                    string[]? workExperiencePlaces,
                    int workExpirienceYearsOtherPlaces,
                    ICollection<DBModelBenefit>? benefits,
                    string description) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        SalaryPerHour = salaryPerHour;
        DateOfEmployment = dateOfEmployment;
        WorkExperiencePlaces = workExperiencePlaces;
        WorkExpirienceYearsOtherPlaces = workExpirienceYearsOtherPlaces;
        Description = description;
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
    public string Description { get; }

    public void IncrementWorkedHours() { WorkedHoursThisMonth++; }
}