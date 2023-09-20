using System.Text.Json.Serialization;
using Domain.Entities.Roles;

namespace Domain.Entities.People;

public class UserEmployee : Human
{
    public UserEmployee(
        string name, 
        string surname,
        string patronymicName,
        DateTime dateOfBirth, 
        Sex sex,
        string login,
        string password,
        IEnumerable<JobTitle> jobTitles ) : base(name, surname, patronymicName, dateOfBirth, sex)
    {
        Login = new string(login);
        Password = new string(password);
        JobTitles = new List<JobTitle>(jobTitles);
    }

    // public UserEmployee(string name, string surname,string patronymicName, string login, string password)
    // {
    //     Login = new string(login);
    //     Password = new string(password);
    //     JobTitles = new List<JobTitle>();
    // }
    public UserEmployee(UserEmployee employee) 
        : base(employee.Name, 
            employee.Surname, 
            employee.PatronymicName, 
            employee.DateOfBirth, 
            employee.Sex)
    {
        Login = new string(employee.Login);
        Password = new string(employee.Password);
        JobTitles = new List<JobTitle>(employee.JobTitles);
    }
    
    public string Login { get; }
    public string Password { get; }

    [JsonIgnore] public IEnumerable<JobTitle> JobTitles { get; }

    public override string ToString()
    {
        return string.Join("\n", Name, Surname, Login);
    }
}
