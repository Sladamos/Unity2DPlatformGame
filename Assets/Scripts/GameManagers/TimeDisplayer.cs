using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimeDisplayer : MonoBehaviour
    {
        private const int SECONDS_IN_MINUTE = 60;
        private const int MILLISECONDS_IN_SECOND = 1000;
        private const int MILLISECONDS_IN_MINUTE = SECONDS_IN_MINUTE * MILLISECONDS_IN_SECOND;

        [SerializeField]
        private TimeManager timeManager;

        private TextMeshProUGUI text;

        private void UpdateDisplayedTime(float secondsPassed)
        {
            if (text != null)
            {
                int millisecondsPassed = (int)(secondsPassed * 1000.0f);
                int minutes = millisecondsPassed / MILLISECONDS_IN_MINUTE;
                int seconds = (millisecondsPassed / MILLISECONDS_IN_SECOND) % SECONDS_IN_MINUTE;
                int milliseconds = millisecondsPassed % MILLISECONDS_IN_SECOND;
                text.SetText(string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds));
            }
        }

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            if (timeManager != null)
            {
                timeManager.TimeUpdated += UpdateDisplayedTime;
            }
        }
    }
}
