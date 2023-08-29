using SystemMonitor.Messages;

namespace SystemMonitor.Exceptions;

public class EnvironmentVariableNotSetException : Exception
{
    public EnvironmentVariableNotSetException(string environmentVariable) : base(
        Errors.Environment.EnvironmentVariableNotSet(environmentVariable))
    {
    }
}