using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TimeDisplayer : MonoBehaviour
    {
        private const int SECONDS_IN_MINUTE = 60;
        private const int MILLISECONDS_IN_SECOND = 1000;
        private const int MILLISECONDS_IN_MINUTE = SECONDS_IN_MINUTE * MILLISECONDS_IN_SECOND;

        [SerializeField]
        private TimeManager timeManager;

        [SerializeField]
        private TMP_Text timeText;

        private void UpdateDisplayedTime(float secondsPassed)
        {
            int seconds = (int)(secondsPassed);
            int minutes = seconds / SECONDS_IN_MINUTE;
            seconds %= SECONDS_IN_MINUTE;
            timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }

        private void Awake()
        {
            if (timeManager != null)
            {
                timeManager.TimeUpdated += UpdateDisplayedTime;
            }
        }
    }
}
