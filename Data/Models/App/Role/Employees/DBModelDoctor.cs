
using Data.Models.Common;
using Data.Models.People.Attribute;
using Data.Models.Polyclinic.Appointment;
using Domain.Entities.People.Attribute;

namespace Data.Models.App.Role.Employees;

public sealed class DBModelDoctor : DBModelEmployee
{
    public DBModelDoctor(string login,
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
                  string dateOfEmployment,
                  string[]? workExperiencePlaces,
                  int workExpirienceYearsOtherPlaces,
                  ICollection<DBModelBenefit>? benefits,
                  string description,
                  ICollection<DBModelAppointment> appointments) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, salaryPerHour, dateOfEmployment, workExperiencePlaces, workExpirienceYearsOtherPlaces, benefits, description)
    {
        Appointments = appointments;
    }
    public ICollection<DBModelAppointment> Appointments { get; set; }
}