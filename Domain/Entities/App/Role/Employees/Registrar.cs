
using Domain.Common;
using Domain.Entities.People.Attribute;

namespace Domain.Entities.App.Role.Employees;


public sealed class Registrar : Employee
{
    public Registrar(string login,
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
                     ICollection<Benefit>? benefits) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces, benefits)
    {
    }
}