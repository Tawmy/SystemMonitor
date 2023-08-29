namespace SystemMonitor.Records;

public record MemoryHealth
{
    internal MemoryHealth(MemoryMetrics metrics, decimal maximumPercentage)
    {
        PercentageUsed = CalcPercentageUsed(metrics);
        IsHealthy = CalcHealth(maximumPercentage);
    }

    public decimal PercentageUsed { get; init; }
    public bool IsHealthy { get; init; }

    private static decimal CalcPercentageUsed(MemoryMetrics metrics)
    {
        return Math.Round(decimal.Divide(metrics.Used, metrics.Total) * 100, 2);
    }

    private bool CalcHealth(decimal maximumPercentage)
    {
        return maximumPercentage >= PercentageUsed;
    }
}