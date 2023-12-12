
using Data.Models.Common;
using Data.Models.People.Attribute;
using Domain.Entities.People.Attribute;

namespace Data.Models.App.Role.Employees;

public sealed class DBModelAdministrator : DBModelEmployee
{
    public DBModelAdministrator(string login,
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
                         string description,
                         ICollection<DBModelBenefit>? benefits) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces, benefits, description)
    {
    }
}