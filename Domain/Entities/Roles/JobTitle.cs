namespace Domain.Entities.Roles;

public abstract class JobTitle
{
    public JobTitle(string login)
    {
        Login = new string(login);
    }

    public JobTitle()
    {
        Login = new string("Login");
    }

    public string Login { get; set; }
}