using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorEmployeeInteractor
{
    private readonly IDoctorEmployeeRepository _doctorRepository;
    private ClientInteractor _clientInteractor;
    public static bool _isAuthorized = false;


    public DoctorEmployeeInteractor(IDoctorEmployeeRepository doctorRepository, IClientRepository clientRepository)
    {
        _doctorRepository = doctorRepository;
        _clientInteractor = new ClientInteractor(clientRepository);
    }

    private void CheckAuthorization()
    {
        if (!_isAuthorized) throw new DoctorEmployeeException(DoctorEmployeeException.Authorization);
    }

    private IEnumerable<Tuple<Client, DateTime>> GetPatients(string login)
    {
        return _clientInteractor.GetByDoctor(Get(login));
    }


    public bool Authorization(string login, string password)
    {
        foreach (var empl in  new List<DoctorEmployee>(_doctorRepository.Read()))
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
        foreach (var empl in new List<DoctorEmployee>(_doctorRepository.Read()))
        {
            if (string.Equals(empl.Login, login))
            {
                doc = new DoctorEmployee(empl);
                doc.Patients = new List<Tuple<Client, DateTime>>(_clientInteractor.GetByDoctor(doc));
                return doc;
            }
        }

        throw new DoctorEmployeeException(DoctorEmployeeException.LoginNotFound);
    }

    public void AddPatient(string login, Client patient, DateTime nextMeetTime)
    {
        CheckAuthorization();
        _clientInteractor.SendToDoctor(Get(login), patient, nextMeetTime);        
    }
    
    public void AddPatient(string login, Tuple<Client, DateTime> client)
    {
        CheckAuthorization();
        _clientInteractor.SendToDoctor(Get(login), client);        
    }
    
    
    public void ChangePassword(string login, string newPassword)
    {
        CheckAuthorization();
        DoctorEmployee doctor = Get(login);
        
        if (newPassword.Length > 5)
        {
            doctor.Password = newPassword;
            _doctorRepository.Update(doctor);
        }
        else
        {
            throw new Exception();
        }
    }
    

    public IObservable<DoctorEmployee> Observe(string login)
    {
        CheckAuthorization();
        return _doctorRepository.ObserveByLogin(login);
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