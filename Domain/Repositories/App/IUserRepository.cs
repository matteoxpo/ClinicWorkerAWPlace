using Domain.Entities.App;

namespace Domain.Repositories.App;


public interface IUserRepository<UserType, ID> : IUpdatable<UserType>, IDeletable<UserType>, IComparable<UserType> where UserType : User
{
    UserType Read(string login, string password);
    public void Delete(string login, string password);
    public void Delete(ID id, string password);
    public void ResetPassword(string loging, string password, string newPassword);
    public void ResetPassword(ID id, string password, string newPassword);
    public Type GetUserSessionType() => typeof(UserType);
}

public class UserRepositoryException : Exception
{
    public UserRepositoryException(string message) : base(message) { }
}