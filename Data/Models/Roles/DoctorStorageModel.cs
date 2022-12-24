using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.Roles;

namespace Data.Models.Roles;

[Serializable]
public class DoctorStorageModel : JobTitleSotrageModel, IConverter<Doctor, DoctorStorageModel>
{
    public DoctorStorageModel(Qualifications category, IEnumerable<string> speciality, IEnumerable<Appointment> appointments,
        string userLogin) : base(userLogin)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Appointments = new List<Appointment>(appointments);
    }

    public DoctorStorageModel() : base("Login")
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Appointments = new List<Appointment>();
    }

    public Qualifications Category { get; set; }
    public IEnumerable<string> Speciality { get; set; }
    [JsonIgnore] public IEnumerable<Appointment> Appointments { get; set; }
   
    public Doctor ConvertToEntity(DoctorStorageModel entity)
    {
        return new Doctor(entity.Category, entity.Speciality, entity.Appointments, entity.Login);
    }

    public DoctorStorageModel ConvertToStorageEntity(Doctor entity)
    {
        return new DoctorStorageModel(entity.Category, entity.Speciality, entity.Appointments, entity.Login);
    }
}