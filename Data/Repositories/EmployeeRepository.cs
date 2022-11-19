using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Data.Models;
using System.Text.RegularExpressions;
namespace Data.Repositories;

public class EmployeeRepository //: IEmployeeRepository
{
    public bool ChangeLogin(string login, string newLogin)
    {
        return true;
    }
    public bool ChangePassword(string login, string newPassword)
    {
        return true;
    }
    public void ChangeSurname(string login, string newSurname) { }
    public void ChangeName(string login, string newName) { }
    public void AddClient(Client newClient) { }

    public void ChangeQualification(string login, Qualifications newQual)
    {

    }

    private bool checkValidation(string value, string oldValue, int minLength)
    {
        return (value.Length >= minLength && !string.Equals(value, oldValue) && Regex.IsMatch(value, @"^[a-zA-Z]+$"));
    }


}

