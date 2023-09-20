using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.ReactiveUI;
using Data.Repositories;
using Domain.Entities;
using Domain.Entities.People;
using Domain.Entities.Roles;

namespace Presentation;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // SetData();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void SetData()
    {
        // try
        // {
        //     var usrRep = UserEmployeeRepository.GetInstance();
        //     var dRep = DoctorRepository.GetInstance();
        //     var clRep = ClientRepository.GetInstance();
        //     var appRep = AppointmentRepository.GetInstance();
        //
        //     for (var i = 0; i < 100; i++)
        //     {
        //         var doc = new Doctor(Qualifications.FirstCategory, new List<string> { $"spec {i}" },
        //             new List<Appointment>(), $"log{i}");
        //         var user = new UserEmployee($"name{i}", $"surname{i}", $"log{i}", $"pas{i}",
        //             new DateTime(2000, 2, 2));
        //         var client = new Client($"Name{i}", $"Surname{i}", new DateTime(2003, 10, 12),$"{i}");
        //         // usrRep.Add(user);
        //         // dRep.Add(doc);
        //         // clRep.Add(client);
        //         // appRep.Add(new Appointment(doc.Login, client.Id, new DateTime(2023, 12, 1, 12, 20, 0),
        //             // new string($"complaints{i}")));
        //     }
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex.Message);
        // }
        //
        // var repository = AnalysisRepository.GetInstance();
        // for (var i = 0; i < 100; i++)
        // {
        //     repository.Add(new Analysis(
        //         $"title{i}",
        //         new TimeSpan(2, 0, 0, 0),
        //         new TimeSpan(0, 0, i + 1, 0),
        //         (i+ 1).ToString()
        //         )
        //     );
        // }

     
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }
}