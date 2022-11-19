
using Domain.Entities.People;
using Domain.Entities;
namespace Domain.Repositories;

public interface IEmployeeRepository
{
    // List<Employee> GetEmployees();

    bool ChangeLogin(string login, string newLogin);
    bool ChangePassword(string login, string newPassword);
    void ChangeSurname(string login, string newSurname);
    void ChangeName(string login, string newName);
    void ChangeQualification(string login, Qualifications newQualification);
    void AddClient(Client newClient);
    // void ChangeClietState(Client newClient);

}

