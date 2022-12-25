using System.Text.Json.Serialization;

namespace Domain.Entities.Roles;

[Serializable]
public class Doctor : JobTitle
{
    public Doctor(Qualifications category, IEnumerable<string> speciality, IEnumerable<Appointment> appointments,
        string userLogin) : base(userLogin)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Appointments = new List<Appointment>(appointments);
    }


    public Qualifications Category { get; }
    public IEnumerable<string> Speciality { get; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; }
}