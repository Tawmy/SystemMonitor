using SystemMonitor.Helper;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class WindowsDiskService : IDiskService
{
    public Task<MemoryMetrics> GetDiskMetricsAsync(string driveLetter)
    {
        var driveInfo = new DriveInfo(driveLetter);

        var total = UnitHelper.ByteToGb(driveInfo.TotalSize);
        var free = UnitHelper.ByteToGb(driveInfo.AvailableFreeSpace);
        var used = total - free;

        return Task.FromResult(new MemoryMetrics(total, used, free));
    }

    public async Task<MemoryHealth> GetDiskHealthAsync(string driveLetter, decimal maximumPercentage)
    {
        var metrics = await GetDiskMetricsAsync(driveLetter);
        return new MemoryHealth(metrics, maximumPercentage);
    }
}