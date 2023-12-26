
using Domain.Entities.Common;
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
                  int id,
                  MedicalPolicy policy,
                  ICollection<Contact> contacts,
                  ICollection<Education>? education,
                  decimal salaryPerHour,
                  DateTime dateOfEmployment,
                  ICollection<Benefit>? benefits,
                  string description,
                  ICollection<Appointment> appointments) : base(login,
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
        Appointments = appointments;
    }
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments.Contains(appointment))
        {
            throw new Exception("");
        }
        Appointments.Add(appointment);
    }
    public ICollection<Appointment> Appointments { get; set; }
}