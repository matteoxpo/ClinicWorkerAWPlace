using Domain.Common;
using Domain.Entities.People.Attribute;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Disease;
using Domain.Entities.Polyclinic.Treatment;

namespace Domain.Entities.App.Role;

public sealed class Client : User
{
    public Client(string login,
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
                  ICollection<Benefit>? benefits,
                  ICollection<Appointment> appointments,
                  ICollection<TreatmentCourse> treatmentCourses) : base(login, password, name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        Appointments = appointments;
        TreatmentCourses = treatmentCourses;
    }

    public ICollection<Appointment> Appointments { get; }
    public ICollection<TreatmentCourse> TreatmentCourses { get; private set; }

    public void AddAppointment(Appointment newAppointment)
    {
        if (newAppointment.ClientID != ID)
        {
            throw new ArgumentException($"Treatment course client ID:{newAppointment.ClientID} is not equal current CLient ID:{ID}");
        }
        Appointments.Add(newAppointment);
    }

    public void AddTreatmentCourse(TreatmentCourse course)
    {
        if (course.ClientID != ID)
        {
            throw new ArgumentException($"New yreatment course client ID:{course.ClientID} is not equal current CLient ID:{ID}");
        }
        TreatmentCourses.Add(course);
    }

    public ICollection<(ICollection<Disease>, DateTime)> GetDiseasesHistory()
    {
        var diseases = new List<(ICollection<Disease>, DateTime)>();
        foreach (var course in TreatmentCourses)
        {
            diseases.Add(course.LastDiagnoses());
        }

        return diseases;
    }

}