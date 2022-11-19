
using System.Xml.Serialization;
namespace Domain.Common.People;

public class User : Human
{
    public string Login { get => _login; set => _login = value; }
    public string Password { get => _password; set => _password = value; }
    protected string _login;
    protected string _password;

    public User(string name, string surname, string password, string login) : base(name, surname)
    {
        _login = new string(login);
        _password = new string(password);
    }


}
