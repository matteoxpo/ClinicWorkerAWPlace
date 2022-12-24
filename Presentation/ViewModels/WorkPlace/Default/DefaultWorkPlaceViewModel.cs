using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;
using Domain.UseCases;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default;

public class DefaultWorkPlaceViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel
{
    private readonly DoctorInteractor _doctorInteractor;
    private readonly ReferenceForAnalysisInteractor _forAnalysisInteractor;

    private ObservableCollection<Client>? _allClients;
    private bool _isCheckedToday;
    private Analysis _selectedAnalyses;

    private string _selectedAnalysesTime;
    private ObservableCollection<ReferenceForAnalysis> _selectedAnalysisTimetable;

    private Client _selectedClient;
    private string _selectedClientNewAppointmentTime;
    private string _selectedClientNewComplaints;
    private ObservableCollection<Client>? _showingClients;
    private ObservableCollection<Client>? _todaysClients;


    public DefaultWorkPlaceViewModel(IScreen hostScreen, string login)
    {
        Login = login;

        HostScreen = hostScreen;

        SelectedClientNewComplaints = null;

        var analysis = AnalysisRepository.GetInstance();

        Analyses = analysis.Read();

        Activator = new ViewModelActivator();

        _forAnalysisInteractor = new ReferenceForAnalysisInteractor(ReferenceForAnalysisRepository.GetInstance());

        _doctorInteractor = new DoctorInteractor(
            ClientRepository.GetInstance(),
            AppointmentRepository.GetInstance(),
            ReferenceForAnalysisRepository.GetInstance(),
            DoctorRepository.GetInstance()
        );


        new UserEmployeeInteractor(
                UserEmployeeRepository.GetInstance())
            .IsUserDoctor(login);

        this.WhenActivated(compositeDisposable =>
            _doctorInteractor
                .Observe(login)
                .Subscribe(UpdateDoctorClients)
                .DisposeWith(compositeDisposable)
        );
        UpdateDoctorClients(login);

        ShowAdditionPatient = new Interaction<AdditionPatientViewModel, Appointment?>();

        AddPatient = ReactiveCommand.CreateFromTask(OnAddExtraAppointment);

        AddAppointment = ReactiveCommand.CreateFromTask(OnAddNextAppointment);

        ShowAllClients = ReactiveCommand.Create(SetAllClientsToShow);

        ShowTodayClients = ReactiveCommand.Create(SetTodaysClientsToShow);

        SetTodaysClientsToShow();

        AddAnalysis = ReactiveCommand.CreateFromTask(OnAddAnalysis);
    }

    private async Task OnAddAnalysis()
    {
        try
        {
            _doctorInteractor.AddAnalysis(
                new ReferenceForAnalysis(
                    SelectedAnalyses,
                    DateTime.ParseExact(SelectedAnalysesTime, "HH:mm dd.MM.yyyy", CultureInfo.CurrentCulture),
                    SelectedClient.Id
                ),
                Login);
        }
        catch (FormatException ex)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Ошибка",
                    ContentMessage = "Неверный формат ввода даты"
                });
            await messageBoxStandardWindow.Show();
        }
        
    }

    private void SetAllClientsToShow()
    {
        ShowingClients = _allClients;
        _isCheckedToday = false;
    }


    private void SetTodaysClientsToShow()
    {
        ShowingClients = _todaysClients;
        _isCheckedToday = true;
    }

    private string Login { get; }
    public ReactiveCommand<Unit, Unit> ShowAllClients { get; }
    public ReactiveCommand<Unit, Unit> ShowTodayClients { get; }
    public Interaction<AdditionPatientViewModel, Appointment?> ShowAdditionPatient { get; }
    public ReactiveCommand<Unit, Unit> AddAppointment { get; }
    public ReactiveCommand<Unit, Unit> AddPatient { get; }
    public ReactiveCommand<Unit, Unit> AddAnalysis { get; }


    public IEnumerable<Analysis> Analyses { get; }


    public ObservableCollection<Client>? ShowingClients
    {
        get => _showingClients;
        set => this.RaiseAndSetIfChanged(ref _showingClients, value);
    }


    public bool IsCheckedToday
    {
        get => _isCheckedToday;
        set => this.RaiseAndSetIfChanged(ref _isCheckedToday, value);
    }

    public Analysis SelectedAnalyses
    {
        get => _selectedAnalyses;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedAnalyses, value);
            SelectedAnalysisTimetable =
                new ObservableCollection<ReferenceForAnalysis>(_forAnalysisInteractor.Read(value.Id));
        }
    }

    public string SelectedAnalysesTime
    {
        get => _selectedAnalysesTime;
        set => this.RaiseAndSetIfChanged(ref _selectedAnalysesTime, value);
    }

    public ObservableCollection<ReferenceForAnalysis> SelectedAnalysisTimetable
    {
        get => _selectedAnalysisTimetable;
        set => this.RaiseAndSetIfChanged(ref _selectedAnalysisTimetable, value);
    }

    public Client SelectedClient
    {
        get => _selectedClient;
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
    }

    public string SelectedClientNewAppointmentTime
    {
        get => _selectedClientNewAppointmentTime;
        set => this.RaiseAndSetIfChanged(ref _selectedClientNewAppointmentTime, value);
    }

    public string SelectedClientNewComplaints
    {
        get => _selectedClientNewComplaints;
        set => this.RaiseAndSetIfChanged(ref _selectedClientNewComplaints, value);
    }

    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; }

    private async Task OnAddExtraAppointment()
    {
        var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(Login));
        if (newAppointment is not null) _doctorInteractor.AddAppointmnet(newAppointment, true);
    }

    private async Task OnAddNextAppointment()
    {
        try
        {
            if (SelectedClientNewAppointmentTime is null) throw new NullReferenceException();

            if (SelectedClientNewComplaints is null) throw new NullReferenceException();

            _doctorInteractor.AddAppointmnet(new Appointment(
                    Login,
                    SelectedClient.Id,
                    DateTime.ParseExact(SelectedClientNewAppointmentTime, "HH:mm dd.MM.yyyy",
                        CultureInfo.CurrentCulture),
                    SelectedClientNewComplaints
                )
            );
        }
        catch (Exception ex)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ContentTitle = "Ошибка",
                    ContentMessage = ex.Message
                });
            await messageBoxStandardWindow.Show();
        }
    }

    private void UpdateDoctorClients(Doctor doctor)
    {
        UpdateDoctorClients(doctor.Login);
    }

    private void UpdateDoctorClients(string doctorLogin)
    {
        _allClients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctorLogin));
        _todaysClients = new ObservableCollection<Client>();
        foreach (var client in _allClients)
            if (client.MeetTime.Year == DateTime.Today.Year &&
                client.MeetTime.Month == DateTime.Today.Month &&
                client.MeetTime.Day == DateTime.Today.Day)
                _todaysClients.Add(client);

        ShowingClients = _todaysClients;
    }
}