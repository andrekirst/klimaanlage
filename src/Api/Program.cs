using System.Device.Gpio;
using Api.Domain.WorkingModes;
using Api.Facades;
using Api.Helpers;
using Api.HostedServices;
using Api.Proxies;
using Api.Services;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        PlatformHelpers.EnsureProcessIsElevated();

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://*:5300", "https://*:5301");

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        builder.Services.AddLogging();
        builder.Services.AddMemoryCache();
        
        builder.Services.AddSingleton<IFanControlService, FanControlService>();
        builder.Services.AddSingleton<IRelayControlService, RelayControlService>();
        builder.Services.AddSingleton<IGpioControllerFacade, GpioControllerFacade>();


        if (PlatformHelpers.IsRunningOnRaspberryPi())
        {
            Console.WriteLine("Register Raspberry PI dependencies");
            builder.Services.AddSingleton<IGpioControllerProxy, GpioControllerProxy>();
            builder.Services.AddSingleton(_ => new GpioController());
        }
        else
        {
            builder.Services.AddSingleton<IGpioControllerProxy, GpioControllerProxyUsingFakeGpioController>();
        }

        builder.Services.AddSingleton<IInitializationService, InitializationService>();
        builder.Services.AddWorkingModes();
        builder.Services.AddHostedService<RunWorkingModeHostedService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var initializationService = scope.ServiceProvider.GetRequiredService<IInitializationService>();
        initializationService.Initialize();

        app.Run();
    }
}