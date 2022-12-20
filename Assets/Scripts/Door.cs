using MIIProjekt.Keys;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour, IMessageReceiverAllKeysCollected
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void OnAllKeysCollected()
        {
        }

        public void AllKeysCollected()
        {
            Logger.Info("All keys collected. Opening door...");
            gameObject.SetActive(false);
        }
    }
}
