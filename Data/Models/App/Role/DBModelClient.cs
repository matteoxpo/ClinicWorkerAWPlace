using Data.Models.Common;
using Data.Models.People.Attribute;
using Data.Models.Polyclinic.Appointment;
using Data.Models.Polyclinic.Treatment;
using Domain.Entities.People.Attribute;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Disease;
using Domain.Entities.Polyclinic.Treatment;

namespace Data.Models.App.Role;

public sealed class DBModelClient : DBModelUser
{
    public DBModelClient(string login,
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
                  ICollection<DBModelBenefit>? benefits,
                  ICollection<DBModelAppointment> appointments,
                  ICollection<DBModelTreatmentCourse> treatmentCourses) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        Appointments = appointments;
        TreatmentCourses = treatmentCourses;
    }

    public ICollection<DBModelAppointment> Appointments { get; set; }
    public ICollection<DBModelTreatmentCourse> TreatmentCourses { get; set; }
}