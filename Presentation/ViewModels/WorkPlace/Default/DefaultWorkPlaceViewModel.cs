using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
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

    public bool IsDoctor { get; }
    public bool IsRegistrar { get; }
    private int id;

    public DefaultWorkPlaceViewModel(IScreen hostScreen, int id)
    {
        HostScreen = hostScreen;

        this.id = id;

        _doctorInteractor = new DoctorInteractor(
            RepositoriesConfigurer.GetRepositoriesConfigurer().GetDoctorRepository(),
            RepositoriesConfigurer.GetRepositoriesConfigurer().GetClientRepository());

        Activator = new ViewModelActivator();


        // UpdateDoctorClients(login);

        AddAppointment = ReactiveCommand.CreateFromTask(OnAddNextAppointment);

        SelectedClientNewAppointmentTime = new string($"ЧЧ:мм ч.{DateTime.Today.Month}.{DateTime.Today.Year}");
    }

    private string Login { get; }
    public ReactiveCommand<Unit, Unit> ShowAllClients { get; }
    public ReactiveCommand<Unit, Unit> ShowTodayClients { get; }
    public ReactiveCommand<Unit, Unit> AddAppointment { get; }

    [Reactive] public IEnumerable<ClientToShow>? ShowingClients { get; private set; }

    [Reactive] public bool IsCheckedAll { get; private set; }

    [Reactive] public ClientToShow? SelectedClient { get; set; }

    [Reactive] public string? SelectedClientNewAppointmentTime { get; set; }

    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; }

    private async Task OnAddExtraAppointment()
    {
        // try
        // {
        //     var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(Login));
        //     if (newAppointment is not null) _doctorInteractor.AddAppointmnet(newAppointment, true);
        // }
        // catch (Exception ex)
        // {
        //     await ShowUncatchedExceptionMessageBox(ex);
        // }
    }

    private async Task OnAddNextAppointment()
    {
        try
        {
            // if (SelectedClient is null) throw new DefaultWorkPlaceViewModelException("Не выбран клиент");

            // if (SelectedClient.MeetTime.Year != DateTime.Today.Year &&
            //     SelectedClient.MeetTime.Month != DateTime.Today.Month &&
            //     SelectedClient.MeetTime.Day != DateTime.Today.Day &&
            //     SelectedClient.MeetTime.Hour != DateTime.Today.Hour)
            //     throw new DefaultWorkPlaceViewModelException("Еще не подошло время прием клиента");

            // if (SelectedClientNewAppointmentTime is null)
            //     throw new DefaultWorkPlaceViewModelException("Не введено время следующей записи");

            // if (SelectedClientNewComplaints is null)
            //     throw new DefaultWorkPlaceViewModelException("Не введно состояние пациента");


            // _doctorInteractor.AddAppointmnet(new Appointment(
            //         Login,
            //         SelectedClient.Id,
            //         DateTime.ParseExact(SelectedClientNewAppointmentTime, "HH:mm dd.MM.yyyy",
            //             CultureInfo.CurrentCulture),
            //         SelectedClientNewComplaints
            //     )
            // );
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

    private void UpdateDoctorClients(Doctor doctor)
    {
        UpdateDoctorClients(doctor.Login);
    }

    private void UpdateDoctorClients(string doctorLogin)
    {
        //     _allClients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctorLogin));
        //     _todaysClients = new ObservableCollection<Client>();
        //     foreach (var client in _allClients)
        //         if (client.MeetTime.Year == DateTime.Today.Year &&
        //             client.MeetTime.Month == DateTime.Today.Month &&
        //             client.MeetTime.Day == DateTime.Today.Day)
        //             _todaysClients.Add(client);

        //     SetAllClientsToShow();
        // }
    }

    public class DefaultWorkPlaceViewModelException : Exception
    {
        public DefaultWorkPlaceViewModelException(string message) : base(message)
        {
        }
    }

    public class ClientToShow
    {
        public Client Client { get; }
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
                    "Дата получения: " + education.Date + " \n");
            }

            return new string(
                "Имя: " + Client.Name + " \n" +
                "Фамилия: " + Client.Surname + " \n" +
                "Отчество: " + Client.PatronymicName + " \n" +
                "Адрес: " + Client.Address + " \n" +
                "Дата рождения: " + Client.DateOfBirth + " \n" +
                educs + benefits
            );
        }
    }

}