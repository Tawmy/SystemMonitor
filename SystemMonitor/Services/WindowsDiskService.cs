using SystemMonitor.Helper;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class WindowsDiskService : IDiskService
{
    public MemoryMetrics GetDiskMetrics(string driveLetter)
    {
        var driveInfo = new DriveInfo(driveLetter);

        var total = UnitHelper.ByteToGb(driveInfo.TotalSize);
        var free = UnitHelper.ByteToGb(driveInfo.AvailableFreeSpace);
        var used = total - free;

        return new MemoryMetrics(total, used, free);
    }

    public MemoryHealth GetDiskHealth(string driveLetter, decimal maximumPercentage)
    {
        var metrics = GetDiskMetrics(driveLetter);
        return new MemoryHealth(metrics, maximumPercentage);
    }
}