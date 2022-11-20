
using Domain.Entities.People;
using Domain.Entities;
namespace Domain.Repositories;

public interface IDoctorEmployeeRepository
{
    void UpdateDoctorEmployee(DoctorEmployee newDoctorEmployee);

    IObservable<DoctorEmployee> ObserveByLogin(string login);
    /*
     var list = desirialize()
     var oldDoc = list[list.FinndIndex(doctorEmployee.Login)]
     list[list.FinndIndex(doctorEmployee.Login)] = newDoc;
     */

    // create read delete
    // del - (login + pass)
    // read -


    // bool ChangeLogin(string login, string newLogin);
    // bool ChangeLogin(DoctorEmployee doctorEmployee, string newLogin);
    // bool ChangePassword(string login, string newPassword);
    // bool ChangePassword(DoctorEmployee doctorEmployee, string newPassword);
    // void ChangeSurname(string login, string newSurname);
    // void ChangeSurname(DoctorEmployee doctorEmployee, string newSurname);
    // void ChangeName(string login, string newName);
    // void ChangeName(DoctorEmployee doctorEmployee, string newName);
    // void AddClient(Client newClient);
    // void AddDocToClient(Client client, DoctorEmployee Doc);

}

