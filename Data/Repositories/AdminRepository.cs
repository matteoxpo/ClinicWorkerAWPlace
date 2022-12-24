using Domain.Entities.Roles;
using Domain.Repositories;

namespace Data.Repositories;

public class AdminRepository : BaseRepository<Admin, AdminStorageModel>, IAdminRepository
{
    private static AdminRepository? _globalRepositoryInstance;

    public AdminRepository(string path) : base(path)
    {
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

    public override bool CompareEntities(Admin entity1, Admin entity2)
    {
        return entity1.Login.Equals(entity2.Login);
    }

    public static AdminRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new AdminRepository(
            "../../../../Data/DataSets/Admin.json");
    }
}