

using Domain.Entities.Common;
using Domain.Entities.People;
using Domain.Entities.People.Attribute;

using Domain.Entities.App.Role;

namespace Domain.Entities.App;

public class User : Human
{
    public string Password { get; set; }
    public User(string login,
                string password,
                string name,
                string surname,
                string patronymicName,
                Address address,
                DateTime dateOfBirth,
                Sex sex,
                int id,
                MedicalPolicy policy,
                ICollection<Contact> contacts,
                ICollection<Education>? education,
                ICollection<Benefit>? benefits) : base(name,
                                                       surname,
                                                       patronymicName,
                                                       address,
                                                       dateOfBirth,
                                                       sex,
                                                       id,
                                                       policy,
                                                       contacts,
                                                       education,
                                                       benefits)
    {
        Login = login ?? throw new NullReferenceException("Login is null");
        Password = password ?? throw new NullReferenceException("Password is null");
    }

    public string Login { get; set; }
}

public class Auth
{

    public Auth(string login, string password)
    {
        Password = password;
        Login = login;
    }

    string Password { get; }
    string Login { get; }

}