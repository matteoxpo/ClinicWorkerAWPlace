
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;

namespace Domain.Entities.App.Role.Employees;

public sealed class Administrator : Employee
{
    public Administrator(string login,
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
                         string description,
                         ICollection<Benefit>? benefits) : base(login,
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
                                                                salaryPerHour,
                                                                dateOfEmployment,
                                                                benefits,
                                                                description)
    {
    }
}