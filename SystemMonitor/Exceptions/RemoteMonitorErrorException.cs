namespace SystemMonitor.Exceptions;

public class RemoteMonitorErrorException : Exception
{
    public RemoteMonitorErrorException(string parameter) : base(parameter)
    {
    }

    public RemoteMonitorErrorException(string parameter, Exception inner) : base(parameter, inner)
    {
    }
}