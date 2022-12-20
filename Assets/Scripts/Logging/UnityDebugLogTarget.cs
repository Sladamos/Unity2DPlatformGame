using System.Text;
using NLog;
using NLog.Targets;
using UnityEngine;

namespace MIIProjekt.Logging
{
    public class UnityDebugLogTarget : Target
    {
        protected override void Write(LogEventInfo logEvent)
        {
            base.Write(logEvent);
            string stringToPrint = new StringBuilder()
                .Append(logEvent.Level.Name)
                .Append(" | ")
                .Append(logEvent.FormattedMessage)
                .ToString();

            if (logEvent.Level >= LogLevel.Error)
            {
                Debug.LogError(stringToPrint);
            }
            else if (logEvent.Level >= LogLevel.Warn)
            {
                Debug.LogWarning(stringToPrint);
            }
            else
            {
                Debug.Log(stringToPrint);
            }
        }
    }
}
