
using System.Collections.Generic;
namespace Domain.Entities.People;
[Serializable]
public class Admin : User
{
    public Admin(string name, string surname, DateTime birthTime,string login, string password) : base(name, surname,birthTime,  login, password) { }
}