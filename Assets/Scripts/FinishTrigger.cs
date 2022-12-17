using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private bool nextLevelIsUnlocked = false;

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                CollisionWithPlayer(collider);
            }
        }

        private void CollisionWithPlayer(Collider2D player)
        {
            if (nextLevelIsUnlocked)
            {
                player.SendMessage("MultiplyScoreByLives", 100);
                player.SendMessage("Finish");
                GameManagers.GameManager.instance.LevelCompleted();
            }
        }

        private void OnAllKeysCollected()
        {
            Logger.Info("Zebrano wszystkie klucze");
            nextLevelIsUnlocked = true;
        }
    }
}
