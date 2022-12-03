using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;
using Data.Repositories;
using Domain.Entities.People;
using Domain.UseCases;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default
{
    public class AdditionPatientViewModel : ReactiveObject
    {
        public string? Nam
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string? Surame
        {
            get => _surname;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
       

        private string? _surname;
        private string? _name;

        public ReactiveCommand<Unit, Tuple<Client, DateTime>> AddPatient { get; } 

        public AdditionPatientViewModel()
        {
            AddPatient = ReactiveCommand.Create(() =>
                {
                    return new Tuple<Client, DateTime>(new Client(), DateTime.Now);
                }
                );
        }

        
    }
}
