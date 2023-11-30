using Domain.Common;
using Domain.Entities.App;

namespace Domain.Repositories.App;

public interface IAuthRepository<ID>
{
    public ICollection<Contact>? GetContacts(string login);
    public bool Auth(string login, string password);
    public bool ResetPassword(string newPassword);
}