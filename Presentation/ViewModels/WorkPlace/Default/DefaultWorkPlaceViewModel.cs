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
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace.Default;

public class DefaultWorkPlaceViewModel : ViewModelBase, IRoutableViewModel, IActivatableViewModel
{
    private readonly DoctorInteractor _doctorInteractor;
    private readonly ReferenceForAnalysisInteractor _forAnalysisInteractor;

    private ObservableCollection<Client>? _allClients;
    private Analysis _selectedAnalyses;

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
            if (SelectedClient is null) throw new DefaultWorkPlaceViewModelException("Не выбран клиент");

            if (SelectedAnalysesTime is null)
                throw new DefaultWorkPlaceViewModelException("Не введно состояние пациента");

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
        catch (DefaultWorkPlaceViewModelException ex)
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

    private void SetAllClientsToShow()
    {
        ShowingClients = _allClients;
        IsCheckedToday = false;
    }


    private void SetTodaysClientsToShow()
    {
        ShowingClients = _todaysClients;
        IsCheckedToday = true;
    }

    private string Login { get; }
    public ReactiveCommand<Unit, Unit> ShowAllClients { get; }
    public ReactiveCommand<Unit, Unit> ShowTodayClients { get; }
    public Interaction<AdditionPatientViewModel, Appointment?> ShowAdditionPatient { get; }
    public ReactiveCommand<Unit, Unit> AddAppointment { get; }
    public ReactiveCommand<Unit, Unit> AddPatient { get; }
    public ReactiveCommand<Unit, Unit> AddAnalysis { get; }


    public IEnumerable<Analysis> Analyses { get; }

    
    [Reactive] public ObservableCollection<Client>? ShowingClients{ get; private set; }

    [Reactive] public bool IsCheckedToday{ get; private set; }

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

    [Reactive] public string? SelectedAnalysesTime { get; set; }

    [Reactive] public ObservableCollection<ReferenceForAnalysis>? SelectedAnalysisTimetable  { get; private set; }

    [Reactive] public Client? SelectedClient { get; set;}

    [Reactive] public string? SelectedClientNewAppointmentTime { get; set; }

    [Reactive] public string? SelectedClientNewComplaints { get; set; }

    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; }

    private async Task OnAddExtraAppointment()
    {
        try
        {
            var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(Login));
            if (newAppointment is not null) _doctorInteractor.AddAppointmnet(newAppointment, true);

        }
        catch (AdditionPatientViewModelException ex)
        {
            await ShowExceptionMessageBox(ex);
        }
        catch (Exception ex)
        {
            await ShowUncatchedExceptionMessageBox(ex);
        }
    }

    private async Task OnAddNextAppointment()
    {
        try
        {
            if (SelectedClient is null) throw new DefaultWorkPlaceViewModelException("Не выбран клиент");
            
            if (SelectedClientNewAppointmentTime is null) throw new DefaultWorkPlaceViewModelException("Не введено время следующей записи");

            if (SelectedClientNewComplaints is null) throw new DefaultWorkPlaceViewModelException("Не введно состояние пациента");
            

            _doctorInteractor.AddAppointmnet(new Appointment(
                    Login,
                    SelectedClient.Id,
                    DateTime.ParseExact(SelectedClientNewAppointmentTime, "HH:mm dd.MM.yyyy", CultureInfo.CurrentCulture),
                    SelectedClientNewComplaints
                )
            );
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

public class DefaultWorkPlaceViewModelException : Exception
{
    public DefaultWorkPlaceViewModelException(string message) : base(message) { }
}