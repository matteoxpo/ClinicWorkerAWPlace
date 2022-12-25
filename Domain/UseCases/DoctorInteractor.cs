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
        return
            from appointment in doctor.Appointments
            let client = _clientInteractor.Get(appointment.ClientId)
            select new Client(
                client.Name,
                client.Surname,
                client.DateOfBirth,
                client.Analyzes,
                client.Appointments,
                client.Id,
                appointment.ClientComplaints,
                appointment.MeetTime
            );
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
            return new Doctor(doc.Category, doc.Speciality, _appointmentInteractor.GetDoctorsAppointments(doc),
                doc.Login);
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