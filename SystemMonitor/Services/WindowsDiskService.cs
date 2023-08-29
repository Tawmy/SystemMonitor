using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class WindowsDiskService : IDiskService
{
    public MemoryMetrics GetDiskMetrics(string driveLetter)
    {
        var driveInfo = new DriveInfo(driveLetter);

        var total = ByteToGb(driveInfo.TotalSize);
        var free = ByteToGb(driveInfo.AvailableFreeSpace);
        var used = total - free;

        return new MemoryMetrics(total, used, free);
    }

    public MemoryHealth GetDiskHealth(string driveLetter, decimal maximumPercentage)
    {
        var metrics = GetDiskMetrics(driveLetter);
        return new MemoryHealth(metrics, maximumPercentage);
    }

    private static decimal ByteToGb(decimal valueInBytes)
    {
        return Math.Round(decimal.Divide(decimal.Divide(decimal.Divide(valueInBytes, 1024), 1024), 1024), 2);
    }
}