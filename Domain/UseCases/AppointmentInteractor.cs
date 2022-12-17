using System.Collections;
using System.Security.Cryptography;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Domain.UseCases;

class AppointmentInteractor
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentInteractor(IAppointmentRepository repository)
    {
        _appointmentRepository = repository;
    }

    public IEnumerable<Appointment> GetAllAppointments()
    {
        return _appointmentRepository.Read();
    }


    public void Add(Doctor doctor, Client client, DateTime meetTime)
    {
        Add(new Appointment(doctor.Login, client.Id, meetTime));
    }

    public void Add(Doctor doctor, Client client, DateTime meetTime, bool unconditionedAppointmentToken)
    {
        Add(new Appointment(doctor.Login, client.Id, meetTime), unconditionedAppointmentToken);
    }


    public void Add(Appointment appointment, bool unconditionedAppointmentToken = false)
    {
        if (!unconditionedAppointmentToken)
        {
            if (GetDoctorsAppointments(appointment.DoctorLogin).Any(meet => meet.MeetTime.Equals(appointment.MeetTime)))
            {
                throw new AppointmentException(AppointmentException.AddingToTheSameTime);
            }

            if (GetClientsAppointments(appointment.ClientId).Any(meet => meet.MeetTime.Equals(appointment.MeetTime)))
            {
                throw new AppointmentException(AppointmentException.BusyClientTime);
            }
        }

        _appointmentRepository.Add(appointment);
    }


    public IEnumerable<Appointment> GetDoctorsAppointments(Doctor doctor)
    {
        return GetDoctorsAppointments(doctor.Login);
    }

    public IEnumerable<Appointment> GetClientsAppointments(Client client)
    {
        return GetClientsAppointments(client.Id);
    }

    public IEnumerable<Appointment> GetDoctorsAppointments(string doctorsLogin)
    {
        var appointments = new List<Appointment>();
        foreach (var appointment in GetAllAppointments())
        {
            if (appointment.DoctorLogin.Equals(doctorsLogin))
            {
                appointments.Add(appointment);
            }
        }

        return appointments;
    }

    public  IEnumerable<Appointment> GetClientsAppointments(string passportSerial)
    {
        return _appointmentRepository.ReadByClient(passportSerial);
    }
    
    public void Delete(Appointment appointment)
    {
        _appointmentRepository.Delete(appointment);
    }

    public IObservable<IEnumerable<Appointment>> ObserveByDoctorLogin(string login)
    {
        return _appointmentRepository.ObserveByDoctor(login);
    }
    
}

public class AppointmentException : Exception
{
    public AppointmentException(string message) : base(message)
    {
    }

    public static string AddingToTheSameTime => "Человек уже записан на это время";
    public static string BusyClientTime => "Клиент уже записан на это время";
}