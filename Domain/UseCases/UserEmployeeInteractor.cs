using System;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Entities.Roles.Doctor;
using Domain.Repositories;

namespace Domain.UseCases;

public class UserEmployeeInteractor
{
    private readonly IUserEmployeeRepository _userRepository;


    public UserEmployeeInteractor(IUserEmployeeRepository userEmployeeRepository)
    {
        _userRepository = userEmployeeRepository;
    }

    public static bool IsAuthorized { get; private set; }

    private void CheckAuthorization()
    {
        if (!IsAuthorized) throw new UserEmployeeException(UserEmployeeException.Authorization);
    }


    public bool Authorization(string login, string password)
    {
        foreach (var empl in _userRepository.ReadOnlyLoginPassword())
            if (string.Equals(empl.Login, login))
            {
                if (string.Equals(empl.Password, password))
                {
                    IsAuthorized = true;
                    return true;
                }

                throw new UserEmployeeException(UserEmployeeException.Authorization);
            }

        throw new UserEmployeeException(UserEmployeeException.LoginNotFound);
    }

    public UserEmployee Get(string login)
    {
        CheckAuthorization();
        foreach (var empl in _userRepository.Read())
            if (string.Equals(empl.Login, login))
                return empl;

        throw new UserEmployeeException(UserEmployeeException.LoginNotFound);
    }


    public void ChangePassword(string login, string oldPassword, string newPassword)
    {
        CheckAuthorization();
        var userEmployee = Get(login);
        if (!userEmployee.Password.Equals(oldPassword))
            throw new UserEmployeeException(UserEmployeeException.OldPasswordErrror);

        if (newPassword.Length <= 5) throw new UserEmployeeException(UserEmployeeException.ShortPasswordException);
        _userRepository.Update(
            new UserEmployee(
                userEmployee.Name,
                userEmployee.Surname,
                userEmployee.Login,
                newPassword,
                userEmployee.JobTitles,
                userEmployee.DateOfBirth
            )
        );
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
            if (job is Doctor)
            {
                return true;
            }
        }

        return false;
    }
}

public class UserEmployeeException : Exception
{
    public UserEmployeeException(string message) : base(message)
    {
    }

    public static string Authorization => "Авторизация не была пройдена";
    public static string LoginNotFound => "Пользователь с таким логином не найдем";
    public static string ShortPasswordException => "Пароль слишком короткий";
    public static string OldPasswordErrror => "Введен некорректный старый пароль";
}