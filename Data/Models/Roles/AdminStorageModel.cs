using Data.Models;

namespace Domain.Entities.Role;

public class AdminStorageModel : JobTitleSotrageModel, IConverter<Admin.Admin, AdminStorageModel>
{
    public AdminStorageModel(string login, uint id) : base(login, id)
    {
    }

    public AdminStorageModel() : base("Login", 0)
    {
    }

    public Admin.Admin ConvertToEntity(AdminStorageModel entity)
    {
        return new Admin.Admin(entity.Login, entity.ID);
    }

    public AdminStorageModel ConvertToStorageEntity(Admin.Admin entity)
    {
        return new AdminStorageModel(entity.Login, entity.ID);
    }
}