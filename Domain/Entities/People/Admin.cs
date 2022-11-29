
using System.Collections.Generic;
namespace Domain.Entities.People;

[Serializable]
public class Admin : User
{
    public Admin(string name, string surname, string password, string login) : base(name, surname, password, login) { }

    //public DoctorEmployee AddEmployee(){}
}