using System;
using MIIProjekt.Collectables;
using MIIProjekt.GameManagers;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class FinishComponent : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action<ICollectable> EndGameCollectableCollected;

        [SerializeField]
        private LevelManager levelManager;

        private void CollectableArrived(ICollectable collectable)
        {
            Logger.Info("Collectable!");
            EndGameCollectableCollected?.Invoke(collectable);
        }

        private void AllCollectablesArrived()
        {
            Logger.Info("Zebrano wszystkie klucze");
            levelManager.InvokeLevelCompleted();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
