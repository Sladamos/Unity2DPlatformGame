using System;
using MIIProjekt.GameManagers;
using NLog;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MIIProjekt
{
    [RequireComponent(typeof(Light2D))]
    public class GlobalLightController : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private Light2D controlledLight;
        private Color colorDelta;
        private float modificatorTimer = 0;

        [SerializeField]
        private Color lowTemperatureColor;

        [SerializeField]
        private float lowTemperatureChangeTime;

        [SerializeField]
        private Color mediumTemperatureColor;

        [SerializeField]
        private float mediumTemperatureChangeTime;

        [SerializeField]
        private Color highTemperatureColor;

        [SerializeField]
        private float highTemperatureChangeTime;

        public void OnLowTemperature()
        {
            ChangeGlobalLightColorGradually(lowTemperatureColor, lowTemperatureChangeTime);
            Logger.Debug("Niska temperatura");
        }

        public void OnMediumTemperature()
        {
            ChangeGlobalLightColorGradually(lowTemperatureColor, lowTemperatureChangeTime);
            Logger.Debug("Srednia temperatura");
        }

        public void OnHighTemperature()
        {
            ChangeGlobalLightColorGradually(lowTemperatureColor, lowTemperatureChangeTime);
            Logger.Debug("Wysoka temperatura!");
        }

        private void Awake()
        {
            controlledLight = GetComponent<Light2D>();
        }

        private void Update()
        {
            if(ItIsNecessaryToChangeTheColor())
            {
                controlledLight.color += colorDelta * Time.deltaTime;
                modificatorTimer = Math.Max(0, modificatorTimer - Time.deltaTime);
            }
        }

        private bool ItIsNecessaryToChangeTheColor()
        {
            return modificatorTimer > 0;
        }

        private void ChangeGlobalLightColorGradually(Color color, float time)
        {
            colorDelta = (color - controlledLight.color) / time;
            modificatorTimer = time;
        }
    }
}
