namespace Domain.Entities.People;

[Serializable]
public class User : Human
{
    public string Login { get; set; }
    public string Password { get ; set; }

    public User(string name, string surname,  DateTime bithTime,string login, string password) : base(name, surname, bithTime)
    {
        Login = new string(login);
        Password = new string(password);
    }

    public User() : base()
    {
        Login = new string("login");
        Password = new string("password");
    }


}
