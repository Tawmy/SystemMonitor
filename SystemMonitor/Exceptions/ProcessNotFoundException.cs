using System.Diagnostics;

namespace SystemMonitor.Exceptions;

public class ProcessNotFoundException : Exception
{
    public ProcessNotFoundException(ProcessStartInfo processStartInfo) : base(processStartInfo.FileName)
    {
    }
}