﻿using System.Runtime.InteropServices;

namespace Api.Helpers;

public static class PlatformHelpers
{
    public static bool IsRunningOnRaspberryPi()
    {
        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        if (!isLinux)
        {
            return false;
        }

        var isArm = RuntimeInformation.ProcessArchitecture == Architecture.Arm ||
                    RuntimeInformation.ProcessArchitecture == Architecture.Arm64;

        if (!isArm)
        {
            return false;
        }

        const string procCpuinfoFile = "/proc/cpuinfo";

        if (!File.Exists(procCpuinfoFile))
        {
            return false;
        }

        var cpuinoFileLines = File.ReadLines(procCpuinfoFile);

        return cpuinoFileLines.Any(cpuinoFileLine => cpuinoFileLine.Contains("Raspberry"));
    }
}