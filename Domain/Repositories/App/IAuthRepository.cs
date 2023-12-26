using Domain.Entities.Common;
using Domain.Entities.App;
using Domain.Repositories.Common;

namespace Domain.Repositories.App;

public interface IAuthRepository
{
    public Task<bool> AuthAsync(string login, string password);
    public Task<int> GetIdByLoginAsync(string login);
    public int GetIdByLogin(string login)
    {
        return GetIdByLoginAsync(login).GetAwaiter().GetResult();
    }
    public bool Auth(string login, string password)
    {
        return AuthAsync(login, password).GetAwaiter().GetResult();
    }
    public Task<bool> ResetPasswordAsync(string newPassword, int id);
    public bool ResetPassword(string newPassword, int id)
    {
        return ResetPasswordAsync(newPassword, id).GetAwaiter().GetResult();
    }
    public Task<bool> IsUserAsync<Type>(int id) where Type : User;
    public bool IsUser<Type>(int id) where Type : User
    {
        return IsUserAsync<Type>(id).GetAwaiter().GetResult();
    }
}