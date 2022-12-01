using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorEmployeeInteractor
{
    private readonly IDoctorEmployeeRepository _repository;

    public DoctorEmployeeInteractor(IDoctorEmployeeRepository repository)
    {
        _repository = repository;
    }
    
    // authorization etc here

    public bool AuthorizationUseCase(string login, string password)
    {
        var empls = new List<DoctorEmployee>(_repository.Read());
        foreach (var empl in empls)
        {
            if (string.Equals(empl.Login, login) && string.Equals(empl.Password, password))
            {
                return true;
            }
        }
        return false;
    }

    public DoctorEmployee Get(string login, string password)
    {
        var empls = new List<DoctorEmployee>(_repository.Read());
        DoctorEmployee? d = null; 
        foreach (var empl in empls)
        {
            if (string.Equals(empl.Login, login) && string.Equals(empl.Password, password))
            {
                d = new DoctorEmployee(empl);
                foreach (var patient in d.Patients)
                {
                    patient.CurrentDoctorMeetTime = patient.Appointments[d.Id].ToString();
                }
                break;
            }
        }
        return d;
    }
    
    // changeName here
    

    public void ChangePasswordUseCase(DoctorEmployee doctor, string newPassword)
    {
        if (newPassword.Length > 5)
        {
            doctor.Password = newPassword;
            _repository.Update(doctor);
        }
        else
        {
            throw new Exception();
        }
    }
    

}