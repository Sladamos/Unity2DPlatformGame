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

        [SerializeField]
        private LevelManager levelManager;

        private void CollectableArrived(ICollectable collectable)
        {
            Logger.Info("Collectable!");
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
