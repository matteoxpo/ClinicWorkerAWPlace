using System.Collections;
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
        foreach (var appintment in GetDoctorsAppointments(doctor))
        {
            if (appintment.MeetTime.Equals(meetTime))
            {
                throw new AppointmentException(AppointmentException.AddingToTheSameTime);
            }
        }

        foreach (var appointment in GetClientsAppointments(client))
        {
            if (appointment.MeetTime.Equals(meetTime))
            {
                throw new AppointmentException(AppointmentException.BusyClientTime);
            }            
        }
        _appointmentRepository.Add(new Appointment(doctor.Login, client.Id.ToString(), meetTime ));
    }

    public IEnumerable<Appointment> GetDoctorsAppointments(Doctor doctor)
    {
        return GetDoctorsAppointments(doctor.Login);
    }
    
    public IEnumerable<Appointment> GetClientsAppointments(Client client)
    {
        return GetClientsAppointments(client.Id);
    } 

    private IEnumerable<Appointment> GetDoctorsAppointments(string doctorsLogin)
    {
        var appointments = new List<Appointment>();
        foreach (var appointment in _appointmentRepository.Read())
        {
            if (appointment.DoctorLogin.Equals(doctorsLogin))
            {
                appointments.Add(appointment);
            }
        }

        return appointments;
    }
    private IEnumerable<Appointment> GetClientsAppointments(string passportSerial)
    {
        return _appointmentRepository.ReadByClient(passportSerial);
    }



    public void Delete(Appointment appointment)
    {
        _appointmentRepository.Delete(appointment);
    }

}

public class AppointmentException : Exception
{
    public AppointmentException(string message) : base(message){}

    public static string AddingToTheSameTime => "Человек уже записан на это время";
    public static string BusyClientTime => "Клиент уже записан на это время";
}