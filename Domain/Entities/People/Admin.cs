
using System.Collections.Generic;
namespace Domain.Entities.People;

public class Admin : User
{
<<<<<<< HEAD
    public Admin(string name, string surname, string password, string login, int id) : base(name, surname, login, password, id) { }

    //public DoctorEmployee AddEmployee(){}
=======
    public Admin(string name, string surname, DateTime BirthTime,string login, string password) : base(name, surname,BirthTime,  login, password) { }
>>>>>>> temporary
}