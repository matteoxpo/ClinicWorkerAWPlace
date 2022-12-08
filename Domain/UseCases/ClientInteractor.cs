using Domain.Entities.People;
using Domain.Repositories;

namespace Domain.UseCases;

public class ClientInteractor
{
    private readonly IClientRepository _repository;

    public ClientInteractor(IClientRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Tuple<Client, DateTime>> GetByDoctor(DoctorEmployee doctor)
    {
        var doctorsPatients = new List<Tuple<Client, DateTime>>();
        foreach (var patient in new List<Client>(_repository.Read()))
        {
            foreach (var doc in patient.Doctors)
            {
                if (string.Equals(doc.Item1.Login, doctor.Login))
                {
                    doctorsPatients.Add(new Tuple<Client, DateTime>(patient, doc.Item2));
                }
            }
        }

        return doctorsPatients;
    }

    public IEnumerable<Client> Get(string name, string surname)
    {
        var clients = new List<Client>();
        foreach (var patient in new List<Client>(_repository.Read()))
        {
            if (string.Equals(patient.Name, name) && string.Equals(patient.Surname, surname))
            {
                clients.Add(patient);
            }
        }

        return clients;
    }
    
    
    public void SendToDoctor(DoctorEmployee doctor, Client patient, DateTime meetTime)
    {
        if (meetTime < DateTime.Now)
        {
            throw new DoctorEmployeeException(ClientException.PastTime);
        }

        // if (patient.Doctors.Select((doc) => doc.Item2.Equals(meetTime)) is not null)
        // {
        //     throw new DoctorEmployeeException(DoctorEmployeeException.TimeCrossing);
        //
        // }
        foreach (var doc in patient.Doctors)
        {
            if (doc.Item2.Equals(meetTime))
            {
                throw new DoctorEmployeeException(DoctorEmployeeException.TimeCrossing);
            }
        }

        // var docs = new List<Tuple<DoctorEmployee, DateTime>>(patient.Doctors);
        // docs.Add(new Tuple<DoctorEmployee, DateTime>(doctor, meetTime));
        patient.Doctors.ToList().Add(new Tuple<DoctorEmployee, DateTime>(doctor, meetTime));
        _repository.Update(patient);

    }
    
    public void SendToDoctor(DoctorEmployee doctor, Tuple<Client, DateTime> client)
    {
        SendToDoctor(doctor, client.Item1, client.Item2);
    }
    
    


}

public class ClientException : Exception
{
    public  ClientException(string message) : base(message) {}
    public static string Authorization => "Авторизация не была пройдена";
    public static string LoginNotFound => "Пользователь с таким логином не найдем";
    public static string TimeCrossing => "На это время уже записан человек";
    public static string PastTime => "Время записи не соответствует текущему";
    
}