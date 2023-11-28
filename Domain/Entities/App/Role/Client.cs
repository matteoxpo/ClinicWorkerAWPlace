using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Disease;
using Domain.Entities.Polyclinic.Treatment;

namespace Domain.Entities.App.Role;

public sealed class Client : UserRole
{
    public Client(string login, uint id, ICollection<TreatmentCourse> treatmentCourses, ICollection<Appointment> appointments) : base(login, id)
    {
        TreatmentCourses = treatmentCourses;
        Appointments = appointments;
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