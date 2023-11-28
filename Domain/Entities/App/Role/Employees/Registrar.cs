
namespace Domain.Entities.App.Role.Employees;


public sealed class Registrar : Employee
{
    public Registrar(string login, uint id, decimal salaryPerHour, DateTime dateOfEmployment, string[]? workExperiencePlaces, int workExpirienceYearsOtherPlaces) : base(login, id, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces)
    {
    }
}