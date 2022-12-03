using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorEmployeeInteractor
{
    private readonly IDoctorEmployeeRepository _repository;
    private bool _isAuthorized;

    public DoctorEmployeeInteractor(IDoctorEmployeeRepository repository)
    {
        _isAuthorized = false;
        _repository = repository;
    }
    
    public bool Authorization(string login, string password)
    {
        var empls = new List<DoctorEmployee>(_repository.Read());
        foreach (var empl in empls)
        {
            if (string.Equals(empl.Login, login) && string.Equals(empl.Password, password))
            {
                _isAuthorized = true;
                return true;
            }
        }
        return false;
    }

    public DoctorEmployee Get(string login)
    {
        if (!_isAuthorized) return null;
        var empls = new List<DoctorEmployee>(_repository.Read());
        DoctorEmployee? d = null; 
        foreach (var empl in empls)
        {
            if (string.Equals(empl.Login, login))
            {
                d = new DoctorEmployee(empl);
                d.Patients.ToList().Sort((p1, p2) =>p1.Item2.CompareTo(p2.Item2));
                break;
            }
        }
        return d;
    }
    
    // changeName here
    public void ChangePassword(DoctorEmployee doctor, string newPassword)
    {
        if (!_isAuthorized) return;
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
    
    public void AddPatient(Client newClient)
    {
        if (!_isAuthorized) return;
        if (newClient == null) throw new ArgumentNullException(nameof(newClient));
    }
    

    public IObservable<DoctorEmployee> Observe(string login)
    {
        if (!_isAuthorized) return null;
        return _repository.ObserveByLogin(login);
    }



}