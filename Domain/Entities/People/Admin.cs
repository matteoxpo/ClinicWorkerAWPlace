
using System.Collections.Generic;
namespace Domain.Entities.People;

[Serializable]
public class Admin : User
{
    public Admin(string name, string surname, string password, string login, int id) : base(name, surname, login, password, id) { }

    //public DoctorEmployee AddEmployee(){}
}