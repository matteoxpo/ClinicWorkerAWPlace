

using Domain.Common;
using Domain.Entities.People;
using Domain.Entities.People.Attribute;

using Domain.Entities.App.Role;
using Domain.Services;

namespace Domain.Entities.App;

public class User : Human
{
    public byte[]? Photo { get; set; }

    private string _password;

    public User(string login,
                string password,
                ICollection<UserRole> roles,
                string name,
                string surname,
                string patronymicName,
                Address address,
                DateTime dateOfBirth,
                Sex sex,
                uint id,
                MedicalPolicy policy,
                ICollection<Contact>? contact = null,
                ICollection<Education>? education = null) : base(name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contact, education)
    {
        Login = login ?? throw new NullReferenceException("Login is null");
        _password = password ?? throw new NullReferenceException("Password is null");
        UserRoles = roles ?? throw new NullReferenceException("User roles is null");
    }

    public void SetPassword(string newPassword, IPasswordValidator validator)
    {
        if (validator.Validate(newPassword))
        {
            _password = newPassword;
            return;
        }
        throw new PasswordValidatingException($"Invalid password: {validator.Reason(newPassword)}");
    }

    public string GetPassword()
    {
        return new string(_password);
    }


    public string Login { get; }

    public ICollection<UserRole> UserRoles { get; }
}