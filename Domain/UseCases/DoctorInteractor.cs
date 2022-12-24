using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.Repositories;

namespace Domain.UseCases;

public class DoctorInteractor
{
    private readonly AppointmentInteractor _appointmentInteractor;
    private readonly ClientInteractor _clientInteractor;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ReferenceForAnalysisInteractor _referenceForAnalysisInteractor;


    public DoctorInteractor(IClientRepository clientRepository, IAppointmentRepository appointmentRepository,
        IReferenceForAnalysisRepository referenceForAnalysisRepository,
        IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        _referenceForAnalysisInteractor = new ReferenceForAnalysisInteractor(referenceForAnalysisRepository);
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

    public void AddAnalysis(ReferenceForAnalysis referenceForAnalysis, string doctorLogin)
    {
        _referenceForAnalysisInteractor.Add(referenceForAnalysis);
        _doctorRepository.Update(Get(doctorLogin));
    }

    public void AddAppointmnet(Appointment appointment, bool unconditionedAppointmentToken = false)
    {
        _appointmentInteractor.Add(appointment, unconditionedAppointmentToken);
        _doctorRepository.Update(Get(appointment.DoctorLogin));
    }

    public IEnumerable<Client> GetClients(Doctor doctor)
    {
        return _clientInteractor.GetDoctorsPatients(doctor);
    }

    public IEnumerable<Client> GetClients(string doctorLogin)
    {
        return GetClients(Get(doctorLogin));
    }

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