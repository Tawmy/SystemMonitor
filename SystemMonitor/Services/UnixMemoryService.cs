using System.Diagnostics;
using SystemMonitor.Exceptions;
using SystemMonitor.Interfaces;
using SystemMonitor.Records;

namespace SystemMonitor.Services;

public class UnixMemoryService : IMemoryService
{
    public Task<MemoryMetrics> GetMemoryMetrics(HttpRequest incomingRequest)
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

        return Task.FromResult(new MemoryMetrics(total, used, free));
    }

    public async Task<MemoryHealth> GetMemoryHealth(HttpRequest incomingRequest, decimal maximumPercentage)
    {
        var metrics = await GetMemoryMetrics(incomingRequest);
        return new MemoryHealth(metrics, maximumPercentage);
    }
}