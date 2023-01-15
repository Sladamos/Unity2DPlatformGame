using MIIProjekt.UI;
using NLog;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TemperatureController : MonoBehaviour
    {
        private const float MaxTemperatureValue = 1.0f;

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private float currentTemperature = 0.0f;

        [Header("Debug")]
        [SerializeField]
        [Range(0.0f, MaxTemperatureValue)]
        private float overrideTemperature = 0.0f;

        [Header("Components")]
        [SerializeField]
        private GlobalLightController globalLightController;

        [SerializeField]
        private SoundsManager soundsManager;

        [SerializeField]
        private DrawingManager drawingManager;

        [Header("Settings")]
        [SerializeField]
        private Color minTemperatureColor = Color.white;

        [SerializeField]
        private Color maxTemperatureColor = Color.red;

        [SerializeField]
        private float lightTemperatureChangeDuration = 0.5f;

        [SerializeField]
        private float temperatureMusicThreshold = 0.45f;

        public float Temperature
        {
            get
            {
                return currentTemperature;
            }
            set
            {
                value = Mathf.Clamp01(value);
                Logger.Debug("Setting temperature to {}", value);

                Color lightColor = getTemperatureColor(value);
                globalLightController.SetLightGradually(lightColor, lightTemperatureChangeDuration);

                currentTemperature = value;

                if (value >= temperatureMusicThreshold)
                {
                    soundsManager.PlayHighTemperatureSong();
                }
            }
        }

        private void OnValidate()
        {
            if (Mathf.Approximately(Temperature, overrideTemperature))
            {
                return;
            }

            Temperature = overrideTemperature;
        }

        private Color getTemperatureColor(float temperature)
        {
            return Color.Lerp(minTemperatureColor, maxTemperatureColor, temperature);
        }
    }
}
