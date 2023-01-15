using MIIProjekt.UI;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TemperatureController : MonoBehaviour
    {
        private const float MaxTemperatureValue = 1.0f;
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
        private float lightTemperatureChangeDuration = 0.2f;

        public void SetTemperature(float newTemperature)
        {
            if (Mathf.Approximately(currentTemperature, newTemperature))
            {
                return;
            }
            
            Color lightColor = getTemperatureColor(newTemperature);
            globalLightController.SetLightGradually(lightColor, lightTemperatureChangeDuration);

            currentTemperature = newTemperature;
        }

        private void OnValidate()
        {
            SetTemperature(overrideTemperature);
        }

        private Color getTemperatureColor(float temperature)
        {
            return Color.Lerp(minTemperatureColor, maxTemperatureColor, temperature);
        }
    }
}
