
using Domain.Common.People;
using Domain.Common;
public interface IAdminRepository
{
    void AddEmployee(Employee worker);
    void ChangeQualification(string login, Qualifications newQualification) { }
    void ChangeQualification(Employee employee, Qualifications newQualification) { }

}