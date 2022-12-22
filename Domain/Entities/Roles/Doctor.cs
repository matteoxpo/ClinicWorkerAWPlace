using System.Text.Json.Serialization;

namespace Domain.Entities.Roles;
[Serializable]
public class Doctor : JobTitle
{
    public Qualifications Category { get; set; }
    public IEnumerable<string> Speciality { get; set; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; set; }

    public Doctor(Qualifications category,IEnumerable<string> speciality, IEnumerable<Appointment> appointments, string userLogin) : base(userLogin)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Appointments = new List<Appointment>(appointments);
    }
    
    public Doctor() : base("Login")
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Appointments = new List<Appointment>();
    }
    
    
}