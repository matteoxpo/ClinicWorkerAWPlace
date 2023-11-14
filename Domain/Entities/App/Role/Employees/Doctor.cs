
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.App.Role.Employees;

public class Doctor : Employee
{
    public Doctor(string login, uint id, decimal salaryPerHour, DateTime dateOfEmployment, ICollection<Appointment> appointments) : base(login, id, salaryPerHour, dateOfEmployment)
    {
        Appointments = appointments;
    }
    public ICollection<Appointment> Appointments { get; set; }

}