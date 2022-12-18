using System.Reactive.Linq;
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


    public DoctorInteractor(IClientRepository clientRepository, IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        _clientInteractor = new ClientInteractor(clientRepository);
        _appointmentInteractor = new AppointmentInteractor(appointmentRepository);
    }

    public IEnumerable<Client> GetDoctorClients(Doctor doctor)
    {
        var clients = new List<Client>();
        foreach (var appointment in doctor.Appointments)
        {
            var client = _clientInteractor.Get(appointment.ClientId);
            client.Complaints = appointment.ClientComplaints;
            client.MeetTime = appointment.MeetTime;
            clients.Add(client);
        }

        var t = clients.OrderBy(client => client.MeetTime);

        return clients;
    }

    public IEnumerable<Client> GetDoctorClients(string login)
    {
        return GetDoctorClients(Get(login));
    }

    public Doctor Get(string login)
    {
        foreach (var doc in _doctorRepository.Read())
        {
            if (!doc.Login.Equals(login)) continue;
            doc.Appointments = _appointmentInteractor.GetDoctorsAppointments(doc);
            return doc;
        }

        throw new DoctorEmployeeException(DoctorEmployeeException.NotFound);
    }

    public void AddAppointmnet(Appointment appointment, bool unconditionedAppointmentToken = false)
    {
        _appointmentInteractor.Add(appointment, unconditionedAppointmentToken);
        var oldDoctor = Get(appointment.DoctorLogin);
        var newAppointments = oldDoctor.Appointments.ToList();
        Doctor newDoctor =
            new Doctor(oldDoctor.Category, oldDoctor.Speciality, newAppointments, oldDoctor.Login);
        
        _doctorRepository.Update(newDoctor);
    }

    public IEnumerable<Client> GetClients(Doctor doctor)
    {
        return _clientInteractor.GetDoctorsPatients(doctor);
    }

    public IEnumerable<Client> GetClients(string doctorLogin) => GetClients(Get(doctorLogin));

    public IObservable<Doctor> Observe(string login)
    {
        return _doctorRepository.ObserveByLogin(login);
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