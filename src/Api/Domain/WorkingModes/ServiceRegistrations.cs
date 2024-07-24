namespace Api.Domain.WorkingModes;

public static class ServiceRegistrations
{
    public static void AddWorkingModes(this IServiceCollection services)
    {
        services.AddSingleton<IWorkingMode, OffWorkingMode>();
        services.AddSingleton<IWorkingMode, AutomaticWithTargetTemperatureAndMaxTimeWorkingMode>();
        services.AddSingleton<IWorkingMode, AutomaticWithTargetTemperatureAndFanSpeedWorkingMode>();
        services.AddSingleton<IWorkingMode, AutomaticWithTargetTemperatureWorkingMode>();
        services.AddSingleton<IWorkingMode, AutomaticWithMaxFanSpeedWorkingMode>();
        services.AddSingleton<IWorkingMode, ManualWithFanSpeedWorkingMode>();

        services.AddSingleton<CurrentWorkingModeSelector>();
    }
}