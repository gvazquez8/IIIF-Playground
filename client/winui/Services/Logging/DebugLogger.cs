using System.Diagnostics;

namespace PlaygroundClient.Services.Logging;
public class DebugLogger : ILoggingService
{
    public void Log(string message)
    {
        Debug.WriteLine(message);
    }

    public void LogError(string message)
    {
        Debug.WriteLine("Error: " + message);
    }
}
