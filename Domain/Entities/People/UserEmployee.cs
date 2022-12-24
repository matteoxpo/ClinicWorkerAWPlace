using System.Text.Json.Serialization;
using Domain.Entities.Roles;

namespace Domain.Entities.People;

[Serializable]
public class UserEmployee
{
    public UserEmployee(string name, string surname, string login, string password, IEnumerable<JobTitle> jobTitles,
        DateTime dateOfBirth)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = dateOfBirth;
        Login = new string(login);
        Password = new string(password);
        JobTitles = new List<JobTitle>(jobTitles);
    }

    public UserEmployee(string name, string surname, string login, string password, DateTime dateOfBirth)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = dateOfBirth;
        Login = new string(login);
        Password = new string(password);
        JobTitles = new List<JobTitle>();
    }


    public UserEmployee(UserEmployee employee)
    {
        Name = new string(employee.Name);
        Surname = new string(employee.Surname);
        DateOfBirth = employee.DateOfBirth;
        Login = new string(employee.Login);
        Password = new string(employee.Password);
        JobTitles = new List<JobTitle>(employee.JobTitles);
    }

    public UserEmployee()
    {
        Name = new string("name");
        Surname = new string("surname");
        DateOfBirth = new DateTime(0);
        Login = new string("login");
        Password = new string("password");
        JobTitles = new List<JobTitle>();
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; }
    public string Login { get; set; }
    public string Password { get; set; }

    [JsonIgnore] public IEnumerable<JobTitle> JobTitles { get; set; }

    public override string ToString()
    {
        return string.Join("\n", Name, Surname, Login);
    }
}
