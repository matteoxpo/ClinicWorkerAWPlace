
using Domain.Entities.People;

namespace Domain.Repositories;

public interface IClientRepository
{
    void Update(Client newClient);
    void Delete(Client oldClient);
    void Add(Client newClient);
    IEnumerable<Client> Read();
}
