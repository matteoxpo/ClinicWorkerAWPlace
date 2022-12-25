using Data.Models;

namespace Domain.Entities.Roles;

public abstract class JobTitleSotrageModel 
{
    public JobTitleSotrageModel(string login)
    {
        Login = new string(login);
    }

    public JobTitleSotrageModel()
    {
        Login = new string("Login");
        
    }

    public string Login { get; set; }
}