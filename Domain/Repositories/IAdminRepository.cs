
using Domain.Entities.People;
using Domain.Entities;
public interface IAdminRepository
{
    void AddEmployee(Employee worker);
    void ChangeQualification(string login, Qualifications newQualification) { }
    void ChangeQualification(Employee employee, Qualifications newQualification) { }

}