using Data.Models;

namespace Domain.Entities.Roles;

public class AdminStorageModel : JobTitleSotrageModel, IConverter<Admin, AdminStorageModel>
{
    public AdminStorageModel(string login) : base(login)
    {
    }

    public AdminStorageModel() : base("Login")
    {
    }

    public Admin ConvertToEntity(AdminStorageModel entity)
    {
        return new Admin(entity.Login);
    }

    public AdminStorageModel ConvertToStorageEntity(Admin entity)
    {
        return new AdminStorageModel(entity.Login);
    }
}