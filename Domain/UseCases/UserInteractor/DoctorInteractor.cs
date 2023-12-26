using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Repositories.App.Role;
using Domain.Repositories.App.Role.Employee;

namespace Domain.UseCases.UserInteractor;


public class DoctorInteractor
{
    private IDoctorRepository _doctorRepository;
    private IClientRepository _clientRepository;

    public DoctorInteractor(IDoctorRepository doctorRepository, IClientRepository clientRepository)
    {
        _doctorRepository = doctorRepository;
        _clientRepository = clientRepository;
    }

    public async Task<(Doctor, Client)> AddAppointment(Appointment appointment, Doctor doctor, Client client)
    {
        doctor.AddAppointment(appointment);
        client.AddAppointment(appointment);
        await _clientRepository.UpdateAsync(client);
        await _doctorRepository.UpdateAsync(doctor);
        return (await _doctorRepository.ReadAsync(doctor.ID), await _clientRepository.ReadAsync(client.ID));
    }
    public async Task<List<Client>> GetDoctorAfterDateClients(int id, DateTime? filterTime = null)
    {
        filterTime ??= DateTime.Now;
        var doc = await _doctorRepository.ReadAsync(id);

        List<Client> clients = new();
        foreach (var appointment in doc.Appointments)
        {
            if (appointment.Date >= filterTime)
            {
                clients.Add(await _clientRepository.ReadAsync(appointment.ClientID));
            }
        }
        return clients;
    }
}