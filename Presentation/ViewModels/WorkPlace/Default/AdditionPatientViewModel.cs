using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.UseCases;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Presentation.ViewModels.WorkPlace.Default;

public class AdditionPatientViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly ClientInteractor _clientInteractor;

    public AdditionPatientViewModel(string login)
    {
        _clientInteractor = new ClientInteractor(ClientRepository.GetInstance());

        Login = login;

        Activator = new ViewModelActivator();

        SetClients();

        IsDataInvalid = false;

        SearchPattient = ReactiveCommand.Create(SetClients);
        AddPatient = ReactiveCommand.CreateFromTask(OnAddPatient);
    }

    private string Login { get; }

    [Reactive] public string? Name { get; set; }

    [Reactive] public string? Surname { get; set; }

    [Reactive] public bool IsDataInvalid { get; private set; }

    [Reactive] public Client SelectedClient { get; set; }


    [Reactive] public ObservableCollection<Client> Clients { get; set; }

    [Reactive] public string ClientComplaints { get; set; }

    public ReactiveCommand<Unit, Appointment> AddPatient { get; }
    public ReactiveCommand<Unit, Unit> SearchPattient { get; }

    public ViewModelActivator Activator { get; }

    private async Task<Appointment?> OnAddPatient()
    {
        try
        {
            if (SelectedClient is null) throw new AdditionPatientViewModelException("Не выбран пациент");
            if (ClientComplaints is null || ClientComplaints.Length == 0)
                throw new AdditionPatientViewModelException("Не введено состояние пациента");
            return new Appointment(Login, SelectedClient.Id, DateTime.Now, ClientComplaints);
        }
        catch (AdditionPatientViewModelException ex)
        {
            await ShowExceptionMessageBox(ex);
        }
        catch (Exception ex)
        {
            await ShowUncatchedExceptionMessageBox(ex);
        }

        return null;
    }

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

public class AdditionPatientViewModelException : Exception
{
    public AdditionPatientViewModelException(string message) : base(message)
    {
    }
}