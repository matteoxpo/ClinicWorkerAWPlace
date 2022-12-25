using System.Text.Json.Serialization;
using Domain.Entities.People;
using Domain.Entities.Roles;

namespace Data.Models.People;

[Serializable]
public class UserEmployeeStorageModel : IConverter<UserEmployee, UserEmployeeStorageModel>
{
    public UserEmployeeStorageModel(string name, string surname, string login, string password,
        IEnumerable<JobTitleSotrageModel> jobTitles,
        DateTime dateOfBirth)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = dateOfBirth;
        Login = new string(login);
        Password = new string(password);
        JobTitles = new List<JobTitleSotrageModel>(jobTitles);
    }

    public UserEmployeeStorageModel(string name, string surname, string login, string password, DateTime dateOfBirth)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = dateOfBirth;
        Login = new string(login);
        Password = new string(password);
        JobTitles = new List<JobTitleSotrageModel>();
    }


    public UserEmployeeStorageModel(UserEmployeeStorageModel employee)
    {
        Name = new string(employee.Name);
        Surname = new string(employee.Surname);
        DateOfBirth = employee.DateOfBirth;
        Login = new string(employee.Login);
        Password = new string(employee.Password);
        JobTitles = new List<JobTitleSotrageModel>(employee.JobTitles);
    }

    public UserEmployeeStorageModel()
    {
        Name = new string("name");
        Surname = new string("surname");
        DateOfBirth = new DateTime(0);
        Login = new string("login");
        Password = new string("password");
        JobTitles = new List<JobTitleSotrageModel>();
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; }
    public string Login { get; set; }
    public string Password { get; set; }

    [JsonIgnore] public IEnumerable<JobTitleSotrageModel> JobTitles { get; set; }

    public UserEmployee ConvertToEntity(UserEmployeeStorageModel entity)
    {
        return new UserEmployee(entity.Name, entity.Surname, entity.Login, entity.Password, entity.DateOfBirth);
    }

    public UserEmployeeStorageModel ConvertToStorageEntity(UserEmployee entity)
    {
        return new UserEmployeeStorageModel(entity.Name, entity.Surname, entity.Login, entity.Password,
            entity.DateOfBirth);
    }

    public override string ToString()
    {
        return string.Join("\n", Name, Surname, Login);
    }
}
