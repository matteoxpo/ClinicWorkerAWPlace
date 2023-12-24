using Domain.Entities.Common;
using Domain.Entities.App;
using Domain.Repositories.Common;

namespace Domain.Repositories.App;

public interface IAuthRepository
{
    IContactRepository ContactRepository { get; }
    public Task<IEnumerable<Contact>> GetContactsAsync(string login);
    public IEnumerable<Contact> GetContacts(string login)
    {
        return GetContactsAsync(login).GetAwaiter().GetResult();
    }
    public Task<bool> AuthAsync(string login, string password);
    public bool Auth(string login, string password)
    {
        return AuthAsync(login, password).GetAwaiter().GetResult();
    }
    public Task<bool> ResetPasswordAsync(string newPassword, int id);
    public bool ResetPassword(string newPassword, int id)
    {
        return ResetPasswordAsync(newPassword, id).GetAwaiter().GetResult();
    }
}