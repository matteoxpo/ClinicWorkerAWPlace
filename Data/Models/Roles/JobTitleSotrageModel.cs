using Data.Models;

namespace Domain.Entities.Roles;

public abstract class JobTitleSotrageModel 
{
    public JobTitleSotrageModel(string login, uint id)
    {
        Login = new string(login);
        ID = id;
    }

    public JobTitleSotrageModel()
    {
        Login = new string("Login");
        ID = 0;
    }

    public string Login { get; set; }
    public uint ID { get; set; }
}