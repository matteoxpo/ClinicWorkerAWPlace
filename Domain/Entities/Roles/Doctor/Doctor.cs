using System.Text.Json.Serialization;

namespace Domain.Entities.Roles.Doctor;

public class Doctor : JobTitle
{
    public Doctor(Qualifications category, 
        IEnumerable<string> speciality, 
        IEnumerable<Appointment> appointments, 
        List<EmployeeExperience> doctorExperiences,
        string userLogin, 
        uint id) : base(userLogin, id)
    {
        Category = category;
        DoctorExperiences = doctorExperiences;
        Speciality = new List<string>(speciality);
        Appointments = new List<Appointment>(appointments);
    }


    public Qualifications Category { get; }
    
    public IEnumerable<string> Speciality { get; }
    
    public List<EmployeeExperience> DoctorExperiences { get; private set; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; }
}