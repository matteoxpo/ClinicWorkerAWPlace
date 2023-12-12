
using Data.Models.Common;
using Domain.Entities.People.Attribute;

namespace Data.Models.App.Role.Employees;


public sealed class DBModelRegistrar : DBModelEmployee
{
    public DBModelRegistrar(string login,
                     string password,
                     string name,
                     string surname,
                     string patronymicName,
                     DBModelAddress address,
                     DateTime dateOfBirth,
                     Sex sex,
                     uint id,
                     MedicalPolicy policy,
                     ICollection<DBModelContact> contacts,
                     ICollection<Education>? education,
                     decimal salaryPerHour,
                     DateTime dateOfEmployment,
                     string[]? workExperiencePlaces,
                     int workExpirienceYearsOtherPlaces,
                     string description,
                     ICollection<Benefit>? benefits) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces, benefits, description)
    {
    }
}