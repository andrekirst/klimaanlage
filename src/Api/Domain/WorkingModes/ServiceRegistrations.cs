namespace Api.Domain.WorkingModes;

public static class ServiceRegistrations
{
    public static void AddWorkingModes(this IServiceCollection services)
    {
        services.AddScoped<IWorkingMode, OffWorkingMode>();
        services.AddScoped<IWorkingMode, AutomaticWithTargetTemperatureAndMaxTimeWorkingMode>();
        services.AddScoped<IWorkingMode, AutomaticWithTargetTemperatureAndFanSpeedWorkingMode>();
        services.AddScoped<IWorkingMode, AutomaticWithTargetTemperatureWorkingMode>();
        services.AddScoped<IWorkingMode, AutomaticWithMaxFanSpeedWorkingMode>();
        services.AddScoped<IWorkingMode, ManualWithFanSpeedWorkingMode>();

        services.AddScoped<CurrentWorkingModeSelector>();
    }
}