﻿using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Collections.Generic;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;

namespace Presentation
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // UserEmployeeRepository usrRep = UserEmployeeRepository.GetInstance();
            // DoctorRepository dRep = DoctorRepository.GetInstance();
            // ClientRepository clRep = ClientRepository.GetInstance();
            // AppointmentRepository appRep = AppointmentRepository.GetInstance();
            //
            // for (int i = 0; i < 5; i++)
            // {
            //     Doctor doc = new Doctor(Qualifications.FirstCategory, new List<string>() { $"spec {i}" }, new List<Appointment>(), $"log{i}");
            //     UserEmployee user = new UserEmployee($"name{i}", $"surname{i}",$"log{i}", $"pas{i}", new DateTime(2000, 2, 2) );
            //     Client client = new Client($"Name{i}", $"Surname{i}", new DateTime(2003, 10, 12), $"complaints{i}", new List<ReferenceForAnalysis>(), $"123{i}" );
            //     usrRep.Add(user);
            //     dRep.Add(doc);
            //     clRep.Add(client);
            //     appRep.Add(new Appointment(doc.Login, client.Id, new DateTime(2023, 12, 1, 12, 20, 0)));
            //
            // }
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
