using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorEmployeeInteractor
{
    private readonly IDoctorEmployeeRepository _repository;
    // сюда приходит реализация из Db.Rep
    public DoctorEmployeeInteractor(IDoctorEmployeeRepository repository)
    {
        _repository = repository;
    }
    // changeName here

    public void ChangePasswordUseCase(DoctorEmployee doctor, string newPassword)
    {
        if (newPassword.Length > 5)
        {
            doctor.Password = newPassword;
            _repository.UpdateDoctorEmployee(doctor);
        }
        else
        {
            throw new Exception();
        }
    }
    

}