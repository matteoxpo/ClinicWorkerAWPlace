using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Domain.Entities.People;
public class DoctorEmployee : User
{
    public Qualifications Category { get; set; }
    public List<Tuple<Client, DateTime>> Patients { get; set; }
    public List<string> Speciality { get; set; }



    public DoctorEmployee() : base()
    {
        Category = Qualifications.FirstCategory;
        Speciality = new List<string>();
        Patients = new List<Tuple<Client, DateTime>>();
    }

    public DoctorEmployee(DoctorEmployee doctorEmployee) : base(doctorEmployee.Name, doctorEmployee.Surname, doctorEmployee.Login , doctorEmployee.Password, doctorEmployee.Id)
    public DoctorEmployee(DoctorEmployee doctorEmployee) : base(doctorEmployee.Name, doctorEmployee.Surname, doctorEmployee.DateOfBirth, doctorEmployee.Login , doctorEmployee.Password)
    {
        Category = doctorEmployee.Category;
        Patients = new List<Tuple<Client, DateTime>>(doctorEmployee.Patients);
        Speciality = new List<string>(doctorEmployee.Speciality);
    }
    
<<<<<<< HEAD
    public DoctorEmployee(string name, string surname,  string login, string password, Qualifications category, List<string> speciality, List<Client> clients, int id)
    : base(name, surname, login, password, id)
=======
    public DoctorEmployee(string name, string surname,  DateTime birthTime,string login, string password, Qualifications category, List<string> speciality, List<Tuple<Client, DateTime>> clients)
    : base(name, surname, birthTime ,login, password)
>>>>>>> temporary
    {
        Category = category;
        Speciality = new List<string>(speciality);
        Patients = new List<Tuple<Client, DateTime>>(clients);
    }
}