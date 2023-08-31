using SystemMonitor.Helper;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class UnixDiskService : IDiskService
{
    public Task<MemoryMetrics> GetDiskMetricsAsync(string path)
    {
        var driveInfo = new DriveInfo(path);

        var total = UnitHelper.ByteToGb(driveInfo.TotalSize);
        var free = UnitHelper.ByteToGb(driveInfo.AvailableFreeSpace);
        var used = total - free;

        return Task.FromResult(new MemoryMetrics(total, used, free));
    }

    public async Task<MemoryHealth> GetDiskHealthAsync(string path, decimal maximumPercentage)
    {
        var metrics = await GetDiskMetricsAsync(path);
        return new MemoryHealth(metrics, maximumPercentage);
    }
}