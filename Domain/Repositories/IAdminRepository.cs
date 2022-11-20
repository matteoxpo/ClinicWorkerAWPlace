
using Domain.Entities.People;
using Domain.Entities;
public interface IAdminRepository
{
    void AddEmployee(DoctorEmployee worker);
    void AddMedicines(Medicines medicines);
    void AddAnalysis(Analysis analysis);
    
    // void ChangeAdminLogin()
    
    void ChangeEmployeeQualification(string login, Qualifications newQualification);
    void ChangeEmployeeQualification(DoctorEmployee doctorEmployee, Qualifications newQualification);
    void ChangeEmployeePassword(string login, string newPassword);
    void ChangeEmployeePassword(DoctorEmployee doctorEmployee, string newPassword);
    void ChangeEmployeeLogin(string login, string newLogin);
    void ChangeEmployeeLogin(DoctorEmployee doctorEmployee, string newLogin);
}