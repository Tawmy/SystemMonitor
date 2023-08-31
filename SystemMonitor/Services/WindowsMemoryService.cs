using System.Diagnostics;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class WindowsMemoryService : IMemoryService
{
    public Task<MemoryMetrics> GetMemoryMetrics()
    {
        string output;

        var info = new ProcessStartInfo
        {
            FileName = "wmic",
            Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value",
            RedirectStandardOutput = true
        };

        using (var process = Process.Start(info))
        {
            if (process == null)
            {
                throw new ProcessNotFoundException(info);
            }

            output = process.StandardOutput.ReadToEnd();
        }

        var lines = output.Trim().Split("\n");
        var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
        var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

        var total = Math.Round(decimal.Divide(decimal.Parse(totalMemoryParts[1]), 1024), 0);
        var free = Math.Round(decimal.Divide(decimal.Parse(freeMemoryParts[1]), 1024), 0);
        var used = total - free;

        return Task.FromResult(new MemoryMetrics(total, used, free));
    }

    public async Task<MemoryHealth> GetMemoryHealth(decimal maximumPercentage)
    {
        var metrics = await GetMemoryMetrics();
        return new MemoryHealth(metrics, maximumPercentage);
    }
}