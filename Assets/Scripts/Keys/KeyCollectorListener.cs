using System.Collections.Generic;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Keys
{
    public class KeyCollectorListener : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private List<string> requiredKeys;

        [SerializeField]
        private KeyCollectorComponent keyCollector;

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (keyCollector == null)
            {
                Logger.Error($"Key collector for instance {name} is not set.");
            }
            else
            {
                keyCollector.KeyCollected += OnKeyCollected;
            }
        }

        public List<string> GetRequiredKeys()
        {
            return requiredKeys;
        }

        private void OnKeyCollected()
        {
            if (AreAllKeysCollected())
            {
                keyCollector.KeyCollected -= OnKeyCollected;
                gameObject.SendMessage("OnAllKeysCollected");
            }
        }

        private bool AreAllKeysCollected()
        {
            foreach (string requiredKey in requiredKeys)
            {
                if (!keyCollector.ContainsKey(requiredKey))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
