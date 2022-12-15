using System.Net.Sockets;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(string path) : base(path) { }

    private static AdminRepository? _globalRepositoryInstance;
    public static AdminRepository GetInstance()
    { 
        return _globalRepositoryInstance ??= new AdminRepository(
            "../../../../Data/DataSets/Admin.json");
    }

    public void Update(Admin nextEntity)
    {
        Change(nextEntity);
    }

    public void Delete(Admin oldEntity)
    {
        Remove(oldEntity);
    }

    public void Add(Admin newEntity)
    {
        Append(newEntity);
    }

    public IEnumerable<Admin> Read()
    {
        return DeserializationJson();
    }

    public override bool CompareEntities(Admin changedEntity, Admin entity)
    {
        return changedEntity.Login.Equals(entity.Login);
    }
}