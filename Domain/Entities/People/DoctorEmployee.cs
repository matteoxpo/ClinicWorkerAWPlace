namespace Domain.Entities.People;

[Serializable]
public class DoctorEmployee : User
{
    public Qualifications Category { get; set; }
    public IEnumerable<Tuple<Client, DateTime>> Patients { get; set; }
    public IEnumerable<string> Speciality { get; set; }



    public DoctorEmployee() : base()
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Patients = new List<Tuple<Client, DateTime>>();
    }

    public DoctorEmployee(DoctorEmployee doctorEmployee) : base(doctorEmployee.Name, doctorEmployee.Surname, doctorEmployee.DateOfBirth, doctorEmployee.Login , doctorEmployee.Password)
    {
        Category = doctorEmployee.Category;
        Patients = new List<Tuple<Client, DateTime>>(doctorEmployee.Patients);
        Speciality = new List<string>(doctorEmployee.Speciality);
    }
    
    public DoctorEmployee(string name, string surname, DateTime birthTime ,string login, string password, Qualifications category, IEnumerable<string> speciality, IEnumerable<Tuple<Client, DateTime>> clients)
    : base(name, surname, birthTime ,login, password)
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Patients = new List<Tuple<Client, DateTime>>(clients);
    }
}