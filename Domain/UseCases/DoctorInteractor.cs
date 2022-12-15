using System.Data.Common;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorInteractor
{
    private readonly ClientInteractor _clientInteractor;
    private readonly IDoctorRepository _doctorRepository;
    private readonly AppointmentInteractor _appointmentInteractor;


    public DoctorInteractor(IClientRepository clientRepository, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        _clientInteractor = new ClientInteractor(clientRepository);
        _appointmentInteractor = new AppointmentInteractor(appointmentRepository);
    }

    public Doctor Get(string login)
    {
        foreach (var doc in _doctorRepository.Read())
        {
            if (doc.Login.Equals(login))
            {
                doc.Appointments = _appointmentInteractor.GetDoctorsAppointments(doc);
                return doc;
            }
        }

        throw new DoctorEmployeeException(DoctorEmployeeException.NotFound);
    }

    public void AddPatient(string login, Client patient, DateTime nextMeetTime)
    {
        SendToDoctor(Get(login), patient, nextMeetTime);
    }


    public void SendToDoctor(string anotherDoctorLogin, Client patient, DateTime meetTime)
    {
        SendToDoctor(Get(anotherDoctorLogin), patient, meetTime);
    }

    public void SendToDoctor(Doctor anotherDoctor, Client patient, DateTime meetTime)
    {
        _appointmentInteractor.Add(anotherDoctor, patient, meetTime);
    }

}

public class DoctorEmployeeException : Exception
    {
        public DoctorEmployeeException(string message) : base(message)
        {
        }

        public static string TimeCrossing => "На это время уже записан человек";
        public static string PastTime => "Время записи не соответствует текущему";
        public static string NotFound => "Не найден доктор с таким логином";
    }