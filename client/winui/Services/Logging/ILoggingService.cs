using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundClient.Services.Logging;
public interface ILoggingService
{
    void Log(string message);
    void LogError(string message);
}
