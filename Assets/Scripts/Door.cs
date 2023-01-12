using System.Collections.Generic;
using MIIProjekt.Collectables;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class Door : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private void DisableDoor()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void CollectableArrived(ICollectable collectable)
        {
            Logger.Info("Collectable arrived: {}", collectable);
        }

        private void AllCollectablesArrived()
        {
            Logger.Info("All keys collected. Opening door...");
            DisableDoor();
        }
    }
}
