
using Domain.Entities.Common;
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
                     int id,
                     MedicalPolicy policy,
                     ICollection<Contact> contacts,
                     ICollection<Education>? education,
                     decimal salaryPerHour,
                     ICollection<Benefit> benefits,
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
                                                salaryPerHour,
                                                benefits,
                                                description)
    {
    }
}