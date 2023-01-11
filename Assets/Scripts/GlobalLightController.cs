using System;
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

        public void ChangeGlobalLightColorGradually(Color color, float time)
        {
            // TODO: Dodaj możliwość płynnej zmiany koloru w świecie
            // Po wywołaniu tej metody obiekt GlobalLightController powinien zapisać obecny kolor na świecie
            // i powinien ją powoli zmieniać w określonym czasie
            throw new NotImplementedException();
        }

        private void Awake()
        {
            controlledLight = GetComponent<Light2D>();
        }
    }
}
