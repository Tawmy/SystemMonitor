namespace SystemMonitor.Exceptions;

public class OperatingSystemNotSupportedException : Exception
{
    public OperatingSystemNotSupportedException(string operatingSystemDescription) : base(operatingSystemDescription)
    {
    }
}