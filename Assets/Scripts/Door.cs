using System.Collections.Generic;
using MIIProjekt.Collectables;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour, ICollectableTriggerTarget
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        public void InvokeCollectableTarget(List<ICollectable> collectables)
        {
            Logger.Info("All keys collected. Opening door...");
            gameObject.SetActive(false);
        }
    }
}
