namespace Domain.Entities.App.Role;

public abstract class UserRole
{
    public UserRole(string login, uint id)
    {
        Login = new string(login);
        ID = id;
    }
    public string Login { get; }

    public uint ID { get; }
}