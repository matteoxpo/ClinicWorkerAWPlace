using Domain.Entities.People;

namespace Domain.Repositories;

public interface IUserEmployeeRepository : IBasePerository<UserEmployee>
{
    IObservable<UserEmployee> ObserveByLogin(string login);
    IEnumerable<UserEmployee> ReadOnlyLoginPassword();
}

