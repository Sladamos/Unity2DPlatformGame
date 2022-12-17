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
            Debug.Log(logEvent.FormattedMessage);
        }
    }
}
