namespace SystemMonitor.Helper;

public static class UnitHelper
{
    public static decimal ByteToGb(decimal valueInBytes)
    {
        return Math.Round(decimal.Divide(decimal.Divide(decimal.Divide(valueInBytes, 1024), 1024), 1024), 2);
    }
}