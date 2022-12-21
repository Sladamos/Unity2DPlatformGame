using MIIProjekt.GameManagers;
using MIIProjekt.Logging;
using NLog;
using TMPro;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimeDisplayer : MonoBehaviour
    {
        private const int SECONDS_IN_MINUTE = 60;

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private TMP_Text timeText;

        [SerializeField]
        private TimeManager timeManager;

        private void UpdateDisplayedTime(float secondsPassed)
        {
            int minutes = (int)(secondsPassed / SECONDS_IN_MINUTE);
            int seconds = (int)(secondsPassed % SECONDS_IN_MINUTE);

            timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            timeText = GetComponent<TMP_Text>();

            if (timeManager != null)
            {
                timeManager.TimeUpdated += UpdateDisplayedTime;
            }
            else
            {
                Logger.Warn("An instance of TimeDisplayer does not have an assigned TimeManager instance. Displayed time will not be updated. Current instance name = {}", name);
            }
        }
    }
}
