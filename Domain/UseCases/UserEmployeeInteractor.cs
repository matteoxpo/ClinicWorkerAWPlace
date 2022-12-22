using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Domain.UseCases;

public class UserEmployeeInteractor 
{
    
    private readonly IUserEmployeeRepository _userRepository;

    public static bool IsAuthorized
    {
        get => _isAuthorized;
        private set => _isAuthorized = value;
    }

    private static bool _isAuthorized;
    

    public UserEmployeeInteractor(IUserEmployeeRepository userEmployeeRepository)
    {
        _userRepository = userEmployeeRepository;
    }

    private void CheckAuthorization()
    {
        if (!IsAuthorized) throw new UserEmployeeException(UserEmployeeException.Authorization);
    }


    public bool Authorization(string login, string password)
    {
        foreach (var empl in _userRepository.ReadOnlyLoginPassword())
        {
            if (string.Equals(empl.Login, login))
            {
                if (string.Equals(empl.Password, password))
                {
                    IsAuthorized = true;
                    return true;
                }

                throw new UserEmployeeException(UserEmployeeException.Authorization);
            }
        }
        throw new UserEmployeeException(UserEmployeeException.LoginNotFound);
    }

    public UserEmployee Get(string login)
    {
        CheckAuthorization();
        foreach (var empl in _userRepository.Read())
        {
            if (string.Equals(empl.Login, login))
            {
                return empl;
            }
        }

        throw new UserEmployeeException(UserEmployeeException.LoginNotFound);
    }
    
    
    public void ChangePassword(string login, string oldPassword,string newPassword)
    {
        CheckAuthorization();
        UserEmployee userEmployee = Get(login);
        if (!userEmployee.Password.Equals(oldPassword))
        {
            throw new UserEmployeeException(UserEmployeeException.OldPasswordErrror);
        }

        if (newPassword.Length <= 5)
        {
            throw new UserEmployeeException(UserEmployeeException.ShortPasswordException);
        }
        userEmployee.Password = newPassword;
        _userRepository.Update(userEmployee);
    }
    

    public IObservable<UserEmployee> Observe(string login)
    {
        CheckAuthorization();
        return _userRepository.ObserveByLogin(login);
    }

    public bool IsUserDoctor(string login)
    {
        var user = Get(login);
        foreach (var job in user.JobTitles)
        {
            if (job.GetType() == typeof(Doctor) )
            {
                return true;
            }
        }

        return false;
    }
    
}

public class UserEmployeeException : Exception
{
    public  UserEmployeeException(string message) : base(message) {}
    public static string Authorization => "Авторизация не была пройдена";
    public static string LoginNotFound => "Пользователь с таким логином не найдем";
    public static string ShortPasswordException => "Пароль слишком короткий";
    public static string OldPasswordErrror => "Введен некорректный старый пароль";

}