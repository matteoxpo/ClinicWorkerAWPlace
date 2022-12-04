using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorEmployeeInteractor
{
    private readonly IDoctorEmployeeRepository _repository;
    public static bool _isAuthorized = false;


    public DoctorEmployeeInteractor(IDoctorEmployeeRepository repository)
    {
        _repository = repository;
    }

    private void CheckAuthorization()
    {
        if (!_isAuthorized) throw new DoctorEmployeeException(DoctorEmployeeException.Authorization);
    }
    
    
    public bool Authorization(string login, string password)
    {
        foreach (var empl in  new List<DoctorEmployee>(_repository.Read()))
        {
            if (string.Equals(empl.Login, login))
            {
                if (string.Equals(empl.Password, password))
                {
                    _isAuthorized = true;
                    return true;
                }
                else
                {
                    throw new DoctorEmployeeException(DoctorEmployeeException.Authorization);
                }
            }
        }
        throw new DoctorEmployeeException(DoctorEmployeeException.LoginNotFound);
    }

    public DoctorEmployee Get(string login)
    {
        CheckAuthorization();
        DoctorEmployee doc; 
        foreach (var empl in new List<DoctorEmployee>(_repository.Read()))
        {
            if (string.Equals(empl.Login, login))
            {
                doc = new DoctorEmployee(empl);
                doc.Patients.ToList().Sort((p1, p2) =>p1.Item2.CompareTo(p2.Item2));
                return doc;
            }
        }

        throw new DoctorEmployeeException(DoctorEmployeeException.LoginNotFound);
    }
    
    // changeName here
    public void ChangePassword(string login, string newPassword)
    {
        CheckAuthorization();
        DoctorEmployee doctor;
        if ((doctor = Get(login)) is null) throw new DoctorEmployeeException("Аунтификация не была пройдена");
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
    
    public void AddPatient(string login, Client newClient, DateTime meetTime)
    {
        CheckAuthorization();
        if (newClient is null) throw new NullReferenceException("Попытка добавления несуществующего клиента");
        
        DoctorEmployee doctor;
        if ((doctor = Get(login)) is null) throw new DoctorEmployeeException(DoctorEmployeeException.Authorization);

        if (meetTime < DateTime.Now)
        {
            throw new DoctorEmployeeException(DoctorEmployeeException.PastTime);
        }

        foreach (var patient in doctor.Patients)
        {
            if (patient.Item2.Equals(meetTime))
            {
                throw new DoctorEmployeeException(DoctorEmployeeException.TimeCrossing);
            }
        }
        
        var patients = new List<Tuple<Client, DateTime>>(doctor.Patients);
        patients.Add(new Tuple<Client, DateTime>(newClient, meetTime));
        doctor.Patients = patients;
        _repository.Update(doctor);
    }
    

    public IObservable<DoctorEmployee> Observe(string login)
    {
        CheckAuthorization();
        return _repository.ObserveByLogin(login);
    }

}

public class DoctorEmployeeException : Exception
{
    public  DoctorEmployeeException(string message) : base(message) {}
    public static string Authorization => "Авторизация не была пройдена";
    public static string LoginNotFound => "Пользователь с таким логином не найдем";
    public static string TimeCrossing => "На это время уже записан человек";
    public static string PastTime => "Время записи не соответствует текущему";
    
}