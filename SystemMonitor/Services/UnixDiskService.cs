using SystemMonitor.Helper;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class UnixDiskService : IDiskService
{
    public MemoryMetrics GetDiskMetrics(string path)
    {
        var driveInfo = new DriveInfo(path);

        var total = UnitHelper.ByteToGb(driveInfo.TotalSize);
        var free = UnitHelper.ByteToGb(driveInfo.AvailableFreeSpace);
        var used = total - free;

        return new MemoryMetrics(total, used, free);
    }

    public MemoryHealth GetDiskHealth(string path, decimal maximumPercentage)
    {
        var metrics = GetDiskMetrics(path);
        return new MemoryHealth(metrics, maximumPercentage);
    }
}