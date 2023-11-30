

using Domain.Common;
using Domain.Entities.People;
using Domain.Entities.People.Attribute;

using Domain.Entities.App.Role;

namespace Domain.Entities.App;

public class User : Human
{
    public byte[]? Photo { get; set; }

    private string _password;

    public User(string login,
                string password,
                string name,
                string surname,
                string patronymicName,
                Address address,
                DateTime dateOfBirth,
                Sex sex,
                uint id,
                MedicalPolicy policy,
                ICollection<Contact> contacts,
                ICollection<Education>? education,
                ICollection<Benefit>? benefits) : base(name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        Login = login ?? throw new NullReferenceException("Login is null");
        _password = password ?? throw new NullReferenceException("Password is null");
    }

    public string Login { get; set; }

}