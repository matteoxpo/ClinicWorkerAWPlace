using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.App.Role.Employee;
using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Entities.Polyclinic.Building;
using Domain.Repositories.App;
using Domain.Repositories.App.Role;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Polyclinic;
using Domain.UseCases.UserInteractor;
using Presentation.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace.Default;

public class DefaultWorkPlaceViewModel : ViewModelBase, IRoutableViewModel, IActivatableViewModel
{
    private ObservableCollection<Client>? _allClients;
    private ObservableCollection<Client>? _todaysClients;
    private DoctorInteractor _doctorInteractor;
    public bool IsDoctor
    {
        get;
    }
    public bool IsRegistrar
    {
        get;
    }
    private int id;

    private IRegistrarRepository? _registrarRepository;

    private IAuthRepository _authRepository;
    private IClientRepository _clientRepository;
    private IAppoinmentRepository _appoinmentRepository;
    private IMedicineClinicRepository _medicineClinicRepository;
    private IDoctorRepository _doctorRepository;

    private IEnumerable<ClientToShow> ReadDoctorClients(int id)
    {
        var clients = _clientRepository.ReadAll();
        var parsedClients = new List<Client>();
        foreach (var client in clients)
        {
            foreach (var appointment in client.Appointments)
            {
                if (appointment.DoctorID == id && appointment.Date >= DateTime.MinValue)
                {
                    parsedClients.Add(client);
                }
            }
        }
        return ClientToShow.FromArray(parsedClients);
    }
    public DefaultWorkPlaceViewModel(IScreen hostScreen, int id)
    {
        HostScreen = hostScreen;

        _appoinmentRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAppointmentRepository();

        _medicineClinicRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetClinicRepository();

        _authRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetAuthRepository();

        _clientRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetClientRepository();

        _doctorRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetDoctorRepository();

        IsDoctor = _authRepository.IsUser<Doctor>(id);
        IsRegistrar = _authRepository.IsUser<Registrar>(id);

        if (IsRegistrar)
        {
            ShowingClients = ClientToShow.FromArray(_clientRepository.ReadAll());
            ShowingDoctors = DoctorToShow.FromArray(_doctorRepository.ReadAll());
        }
        else if (IsDoctor)
        {
            ShowingClients = ReadDoctorClients(id);
        }

        _registrarRepository = RepositoriesConfigurer.GetRepositoriesConfigurer().GetRegistrarRepository();

        var reg = _registrarRepository.Read(id);

        this.id = id;

        _doctorInteractor = new DoctorInteractor(
            RepositoriesConfigurer.GetRepositoriesConfigurer().GetDoctorRepository(),
            RepositoriesConfigurer.GetRepositoriesConfigurer().GetClientRepository()
        );

        Activator = new ViewModelActivator();

        AddAppointment = ReactiveCommand.CreateFromTask(OnAddNextAppointment);

        SelectedClientNewAppointmentTime = new string($"ЧЧ:мм ч.{DateTime.Today.Month}.{DateTime.Today.Year}");
    }

    private string Login { get; }
    public ReactiveCommand<Unit, Unit> AddAppointment { get; }

    [Reactive] public IEnumerable<ClientToShow>? ShowingClients { get; private set; }

    [Reactive] public ClientToShow? SelectedClient { get; set; }
    [Reactive] public DoctorToShow? SelectedDoctor { get; set; }

    [Reactive] public IEnumerable<DoctorToShow>? ShowingDoctors { get; set; }
    [Reactive] public string? SelectedClientNewAppointmentTime { get; set; }

    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; }

    private async Task OnAddNextAppointment()
    {
        try
        {
            if (SelectedClient is null)
            {
                throw new DefaultWorkPlaceViewModelException("Не выбран клиент");
            }
            DateTime dateTime = DateTime.Parse(SelectedClientNewAppointmentTime!);
            var Polyclinic = _medicineClinicRepository.Read(1);

            if (IsDoctor)
            {

                var appointment = new Appointment(id, SelectedClient.Client.ID, dateTime, -1, new MeetPlace(Polyclinic, Polyclinic.Cabinets.First()));
                SelectedClient.Client.AddAppointment(appointment);
                _appoinmentRepository.Add(appointment);
                ShowingClients = ReadDoctorClients(id);
            }
            if (IsRegistrar)
            {
                if (SelectedDoctor is null)
                {
                    throw new DefaultWorkPlaceViewModelException("Не выбран доктор");
                }
                var appointment = new Appointment(SelectedDoctor.Doctor.ID, SelectedClient.Client.ID, dateTime, -1, new MeetPlace(Polyclinic, Polyclinic.Cabinets.First()));
                _appoinmentRepository.Add(appointment);
            }
        }
        catch (FormatException ex)
        {
            await ShowExceptionMessageBox("Неверный формат ввода даты");
        }
        catch (DefaultWorkPlaceViewModelException ex)
        {
            await ShowExceptionMessageBox(ex);
        }
        catch (Exception ex)
        {
            await ShowUncatchedExceptionMessageBox(ex);
        }
    }



    public class DefaultWorkPlaceViewModelException : Exception
    {
        public DefaultWorkPlaceViewModelException(string message) : base(message)
        {
        }
    }

    public class DoctorToShow
    {
        public Doctor Doctor { get; }
        public DoctorToShow(Doctor doctor)
        {
            Doctor = doctor;
        }
        public static IEnumerable<DoctorToShow> FromArray(IEnumerable<Doctor> doctors)
        {
            var clientsToShow = new List<DoctorToShow>();
            foreach (var doctor in doctors)
            {
                clientsToShow.Add(new DoctorToShow(doctor));
            }
            return clientsToShow;
        }
        public override string ToString()
        {
            return new string(
                "Имя: " + Doctor.Name +
                "\n Фамилия:" + Doctor.Surname +
                "\n Описание: " + Doctor.Description
                );
        }
    }

    public class ClientToShow
    {
        public Client Client { get; }

        public static IEnumerable<ClientToShow> FromArray(IEnumerable<Client> clients)
        {
            var clientsToShow = new List<ClientToShow>();
            foreach (var client in clients)
            {
                clientsToShow.Add(new ClientToShow(client));
            }
            return clientsToShow;
        }
        public ClientToShow(Client client)
        {
            Client = client;
        }
        public override string ToString()
        {
            StringBuilder benefits = new("Льготы: \n");
            foreach (var benefit in Client.Benefits!)
            {
                benefits.Append(
                    "Название: " + benefit.Type + " \n" +
                    "Описание: " + benefit.Description + " \n" +
                    "Скидка: " + benefit.Discount + " \n");
            }

            StringBuilder educs = new("Образование: \n");
            foreach (var education in Client.Education!)
            {
                benefits.Append(
                    "Номер документа: " + education.Number + " \n" +
                    "Серия документа: " + education.Serial + " \n" +
                    "Дата получения образования: " + education.Date + " \n");
            }

            return new string(
                "Имя: " + Client.Name + " \n" +
                "Фамилия: " + Client.Surname + " \n" +
                "Отчество: " + Client.PatronymicName + " \n" +
                "Адрес: " + Client.Address.City + ", " + Client.Address.Street + ", " + Client.Address.ZipCode + " \n" +
                "Дата рождения: " + Client.DateOfBirth + " \n" +
                educs + benefits
            );
        }
    }

}