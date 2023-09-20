namespace Domain.Entities.Roles;

public abstract class JobTitle
{
    public JobTitle(string login, uint id)
    {
        Login = new string(login);
        ID = id;
    }
    public string Login { get;}
    
    public uint ID { get; }
}