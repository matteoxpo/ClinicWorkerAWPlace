
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.App.Role.Employees;

public sealed class Doctor : Employee
{
    public Doctor(string login, uint id, decimal salaryPerHour, DateTime dateOfEmployment, string[]? workExperiencePlaces, int workExpirienceYearsOtherPlaces, ICollection<Appointment> appointments) : base(login, id, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces)
    {
        Appointments = appointments;
    }
    public ICollection<Appointment> Appointments { get; set; }
}