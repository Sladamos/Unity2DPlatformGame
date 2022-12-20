using System;
using MIIProjekt.Logging;
using MIIProjekt.Keys;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour, IMessageReceiverAllKeysCollected
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
                player.SendMessage("Finish");

                throw new NotImplementedException();
                // GameManagers.GameManager.instance.LevelCompleted();
            }
        }

        public void AllKeysCollected()
        {
            Logger.Info("Zebrano wszystkie klucze");
            nextLevelIsUnlocked = true;
        }
    }
}
