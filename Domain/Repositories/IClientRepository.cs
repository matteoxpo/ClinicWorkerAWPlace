
using Domain.Entities.People;
using Domain.Entities;
namespace Domain.Repositories;

public interface IClientRepository
{
    void Update(Client newClient);
    void Delete(Client oldClient);
    void Add(Client newClient);
    List<Client> Read();
}
