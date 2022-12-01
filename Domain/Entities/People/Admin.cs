
using System.Collections.Generic;
namespace Domain.Entities.People;

public class Admin : User
{
    public Admin(string name, string surname, DateTime BirthTime,string login, string password) : base(name, surname,BirthTime,  login, password) { }
}