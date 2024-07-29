using System.Device.Gpio;
using System.Device.I2c;
using Api.Domain.WorkingModes;
using Api.Facades;
using Api.Hardware.Displays;
using Api.Helpers;
using Api.HostedServices;
using Api.Proxies;
using Api.Services;
using Iot.Device.Graphics;
using Iot.Device.Graphics.SkiaSharpAdapter;

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
            //builder.Services.AddKeyedSingleton(nameof(Ssd1306Size128X64), (_, _) => I2cDevice.Create(new I2cConnectionSettings(1, 0x3C)));
            //builder.Services.AddSingleton(provider =>
            //{
            //    var i2CDevice = provider.GetRequiredKeyedService<I2cDevice>(nameof(Ssd1306Size128X64));
            //    return new Ssd1306Size128X64(i2CDevice);
            //});
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