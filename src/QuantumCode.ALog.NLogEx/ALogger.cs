using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace QuantumCode.ALog.NLogEx
{
    public class ALogger
    {
        public Logger Logger { get; internal set; }

        public string Creator { get; set; }

        public ALogger()
        {
            
        }

        private LogEventInfo CreateLogEventInfo(LogLevel level, string action, string message)
        {
            LogEventInfo eventInfo = new LogEventInfo(level, Creator, message);

            eventInfo.Properties["action"] = action;
            eventInfo.Properties["creator"] = Creator;

            return eventInfo;
        }

        public void Trace(string action, string message)
        {
            Logger.Log(CreateLogEventInfo(LogLevel.Trace, action, message));
        }

        public void Debug(string action, string message)
        {
            Logger.Log(CreateLogEventInfo(LogLevel.Debug, action, message));
        }

        public void Info(string action, string message)
        {
            Logger.Log(CreateLogEventInfo(LogLevel.Info, action, message));
        }

        public void Warn(string action, string message)
        {
            Logger.Warn(CreateLogEventInfo(LogLevel.Warn, action, message));
        }

        public void Error(string action, string message)
        {
            Logger.Log(CreateLogEventInfo(LogLevel.Error, action, message));
        }

        public void Fatal(string action, string message)
        {
            Logger.Log(CreateLogEventInfo(LogLevel.Fatal, action, message));
        }

        public void Exception(string message, Exception eX)
        {
            Logger.ErrorException(message, eX);
        }
    }
}
