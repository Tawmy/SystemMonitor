using System.Diagnostics;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class UnixMemoryService : IMemoryService
{
    public MemoryMetrics GetMemoryMetrics()
    {
        string output;

        var info = new ProcessStartInfo("free -m")
        {
            FileName = "/bin/bash",
            Arguments = "-c \"free -m\"",
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

        var lines = output.Split("\n");
        var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var total = int.Parse(memory[1]);
        var used = int.Parse(memory[2]);
        var free = int.Parse(memory[3]);

        return new MemoryMetrics(total, used, free);
    }

    public MemoryHealth GetMemoryHealth(decimal maximumPercentage)
    {
        var metrics = GetMemoryMetrics();

        var percentageUsed = Math.Round(decimal.Divide(metrics.Used, metrics.Total) * 100, 2);
        var isHealthy = maximumPercentage >= percentageUsed;

        return new MemoryHealth(percentageUsed, isHealthy);
    }
}