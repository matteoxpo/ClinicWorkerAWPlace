using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.UseCases;
using DynamicData;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default;

public class AdditionPatientViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ClientInteractor _clientInteractor;

    private string _clientComplaints;
    private ObservableCollection<Client>? _clients;
    private bool _isDataInvalid;
    private string? _name;
    private Client? _selectedClient;


    private string? _surname;

    public AdditionPatientViewModel(string login)
    {
        _clientInteractor = new ClientInteractor(ClientRepository.GetInstance());

        Activator = new ViewModelActivator();

        Clients = new ObservableCollection<Client>();

        SetClients();

        IsDataInvalid = false;

        SearchPattient = ReactiveCommand.Create(SetClients);
        AddPatient = ReactiveCommand.Create(() =>
            new Appointment(login, SelectedClient.Id, DateTime.Now, ClientComplaints)
        );
    }

    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string? Surname
    {
        get => _surname;
        set => this.RaiseAndSetIfChanged(ref _surname, value);
    }

    public bool IsDataInvalid
    {
        get => _isDataInvalid;
        set => this.RaiseAndSetIfChanged(ref _isDataInvalid, value);
    }


    public Client SelectedClient
    {
        get => _selectedClient;
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
    }


    public ObservableCollection<Client> Clients
    {
        get => _clients;
        private set => this.RaiseAndSetIfChanged(ref _clients, value);
    }

    public string ClientComplaints
    {
        get => _clientComplaints;
        set => this.RaiseAndSetIfChanged(ref _clientComplaints, value);
    }

    public ReactiveCommand<Unit, Appointment> AddPatient { get; }
    public ReactiveCommand<Unit, Unit> SearchPattient { get; }

    public ViewModelActivator Activator { get; }

    private void SetClients()
    {
        if (Name is null || Surname is null)
        {
            Clients = new ObservableCollection<Client>(_clientInteractor.Get());
            return;
        }

        var clients =
            new ObservableCollection<Client>(_clientInteractor.Get(Name, Surname));

        if (!clients.Any())
        {
            clients.AddRange(_clientInteractor.Get());
            IsDataInvalid = true;
        }
        else
        {
            IsDataInvalid = false;
        }

        Clients = clients;
    }
}