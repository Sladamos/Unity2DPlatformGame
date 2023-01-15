using MIIProjekt.GameManagers;
using MIIProjekt.UI;
using UnityEngine;

namespace MIIProjekt
{
    public class TemperatureTriggerHandler : MonoBehaviour
    {
        private const float LowTemperature = 0.2f;
        private const float MediumTemperature = 0.5f;
        private const float HighTemperature = 0.9f;
        
        private float currentTemperature = 0.0f;

        [SerializeField]
        private TemperatureController temperatureController;

        public void OnTriggerLowTemperature(Collider2D collider)
        {
            SetTemperature(LowTemperature);
        }

        public void OnTriggerMediumTemperature(Collider2D collider)
        {
            SetTemperature(MediumTemperature);
        }

        public void OnTriggerHighTemperature(Collider2D collider)
        {
            SetTemperature(HighTemperature);
        }

        private void SetTemperature(float temperature)
        {
            if (currentTemperature > temperature)
            {
                return;
            }

            currentTemperature = temperature;

            temperatureController.SetTemperature(temperature);  
        }
    }
}
