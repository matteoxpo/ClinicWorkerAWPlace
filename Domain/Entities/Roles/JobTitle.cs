namespace Domain.Entities.Roles;

public abstract class JobTitle
{
    public string Login { get; set; }

    public JobTitle(string login)
    {
        Login = new string(login);
    }

    public JobTitle()
    {
        Login = new string("Login");
    }
}