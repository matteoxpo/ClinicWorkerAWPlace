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
        
        ShowAnalysisResult = new Interaction<AnalysisResultViewModel, Unit>();
        

        OpenAddingExtraAppointment = ReactiveCommand.CreateFromTask(OnAddExtraAppointment);

        OpenShowAnalysisResult = ReactiveCommand.CreateFromTask(OnShowAnalysisResult);

        AddAppointment = ReactiveCommand.CreateFromTask(OnAddNextAppointment);

        ShowAllClients = ReactiveCommand.Create(SetAllClientsToShow);

        ShowTodayClients = ReactiveCommand.Create(SetTodaysClientsToShow);

   

        SetTodaysClientsToShow();

        AddAnalysis = ReactiveCommand.CreateFromTask(OnAddAnalysis);

        SelectedClientNewAppointmentTime = new string($"ЧЧ:мм ч.{DateTime.Today.Month}.{DateTime.Today.Year}");

        SelectedAnalysesTime = new string($"ЧЧ:мм ч.{DateTime.Today.Month}.{DateTime.Today.Year}");
    }

  
    private string Login { get; }
    public ReactiveCommand<Unit, Unit> ShowAllClients { get; }
    public ReactiveCommand<Unit, Unit> ShowTodayClients { get; }
    public Interaction<AdditionPatientViewModel, Appointment?> ShowAdditionPatient { get; }
    public Interaction<AnalysisResultViewModel, Unit> ShowAnalysisResult { get; }
    public ReactiveCommand<Unit, Unit> AddAppointment { get; }
    public ReactiveCommand<Unit, Unit> OpenAddingExtraAppointment { get; }
    public ReactiveCommand<Unit, Unit> AddAnalysis { get; }
    public ReactiveCommand<Unit, Unit> OpenShowAnalysisResult { get; }
    


    public IEnumerable<Analysis> Analyses { get; }


    [Reactive] public ObservableCollection<Client>? ShowingClients { get; private set; }

    [Reactive] public bool IsCheckedAll { get; private set; }

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

    [Reactive] public ObservableCollection<ReferenceForAnalysis>? SelectedAnalysisTimetable { get; private set; }

    [Reactive] public Client? SelectedClient { get; set; }

    [Reactive] public string? SelectedClientNewAppointmentTime { get; set; }

    [Reactive] public string? SelectedClientNewComplaints { get; set; }

    public ViewModelActivator Activator { get; }
    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; }

    private ReferenceForAnalysis _selectedClientAnalys;

    public ReferenceForAnalysis SelectedClientAnalys
    {
        get => _selectedClientAnalys;
        set
        {
            IsAnalyzisSelected = value is not null;
            this.RaiseAndSetIfChanged(ref _selectedClientAnalys, value);
        }
    }
    [Reactive] public bool IsAnalyzisSelected { get; set;}

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
                Login
            );
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

    private void SetAllClientsToShow()
    {
        ShowingClients = _allClients;
        IsCheckedAll = true;
    }


    private void SetTodaysClientsToShow()
    {
        ShowingClients = _todaysClients;
        IsCheckedAll = false;
    }

    private async Task OnShowAnalysisResult()
    {
        try
        {
            if (SelectedAnalyses is null)
                throw new DefaultWorkPlaceViewModelException("Не выбран анализ, который будет просматриваться");
            await ShowAnalysisResult.Handle(new AnalysisResultViewModel(SelectedClientAnalys));
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

    
    private async Task OnAddExtraAppointment()
    {
        try
        {
            var newAppointment = await ShowAdditionPatient.Handle(new AdditionPatientViewModel(Login));
            if (newAppointment is not null) _doctorInteractor.AddAppointmnet(newAppointment, true);
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

            if (SelectedClient.MeetTime.Year != DateTime.Today.Year &&
                SelectedClient.MeetTime.Month != DateTime.Today.Month &&
                SelectedClient.MeetTime.Day != DateTime.Today.Day &&
                SelectedClient.MeetTime.Hour != DateTime.Today.Hour)
                throw new DefaultWorkPlaceViewModelException("Еще не подошло время прием клиента");

            if (SelectedClientNewAppointmentTime is null)
                throw new DefaultWorkPlaceViewModelException("Не введено время следующей записи");

            if (SelectedClientNewComplaints is null)
                throw new DefaultWorkPlaceViewModelException("Не введно состояние пациента");


            _doctorInteractor.AddAppointmnet(new Appointment(
                    Login,
                    SelectedClient.Id,
                    DateTime.ParseExact(SelectedClientNewAppointmentTime, "HH:mm dd.MM.yyyy",
                        CultureInfo.CurrentCulture),
                    SelectedClientNewComplaints
                )
            );
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
        _allClients = new ObservableCollection<Client>(_doctorInteractor.GetDoctorClients(doctorLogin));
        _todaysClients = new ObservableCollection<Client>();
        foreach (var client in _allClients)
            if (client.MeetTime.Year == DateTime.Today.Year &&
                client.MeetTime.Month == DateTime.Today.Month &&
                client.MeetTime.Day == DateTime.Today.Day)
                _todaysClients.Add(client);

        SetAllClientsToShow();
    }
}

public class DefaultWorkPlaceViewModelException : Exception
{
    public DefaultWorkPlaceViewModelException(string message) : base(message)
    {
    }
}