namespace Domain.Entities.Roles;

public abstract class JobTitle
{
    public JobTitle(string login)
    {
        Login = new string(login);
    }
    public string Login { get;}
}