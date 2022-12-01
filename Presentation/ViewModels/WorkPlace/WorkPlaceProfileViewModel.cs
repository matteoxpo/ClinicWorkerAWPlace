using System;
using System.Reactive;
using ReactiveUI;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.People;

namespace Presentation.ViewModels.WorkPlace
{
    public class WorkPlaceProfileViewModel : ReactiveObject, IRoutableViewModel
    {
        private DoctorEmployee self;
        public string Name => self.Name;

        public string Surname => self.Surname;
        

        public string Category
        {
            get
            {
                switch (self.Category)
                {
                    case Qualifications.FirstCategory:
                        return "Первая категория";
                    case Qualifications.SecondCategory:
                        return "Вторая категория";
                    case Qualifications.HighestCategory:
                        return "Высшая категория";
                }
                return "Нет данных о категории";
            }
        }
        
        


        public WorkPlaceProfileViewModel(IScreen hostScreen, DoctorEmployee doctorEmployee)
        {
            self = doctorEmployee;
            
            HostScreen = hostScreen;
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        // public RoutingState Router { get; } = new RoutingState();


    }
}
