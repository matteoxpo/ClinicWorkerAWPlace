
using Domain.Common;
using Domain.Entities.People.Attribute;
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.App.Role.Employees;

public sealed class Doctor : Employee
{
    public Doctor(string login,
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
                  ICollection<Benefit>? benefits,
                  ICollection<Appointment> appointments) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces, benefits)
    {
        Appointments = appointments;
    }

    // public Doctor(string login, uint id, decimal salaryPerHour, DateTime dateOfEmployment, string[]? workExperiencePlaces, int workExpirienceYearsOtherPlaces, ICollection<Appointment> appointments) : base(login, id, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces)
    // {
    //     Appointments = appointments;
    // }
    public ICollection<Appointment> Appointments { get; set; }
}