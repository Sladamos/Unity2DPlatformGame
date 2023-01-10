using System;
using MIIProjekt.Collectables.Keys;
using MIIProjekt.Extensions;
using MIIProjekt.GameManagers;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour, IMessageReceiverAllKeysCollected
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private bool nextLevelIsUnlocked = false;

        [SerializeField]
        private LevelManager levelManager;

        public void AllKeysCollected()
        {
            Logger.Info("Zebrano wszystkie klucze");
            nextLevelIsUnlocked = true;
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                if (nextLevelIsUnlocked)
                {
                    collider.SendMessage("Finish");
                    levelManager.VerifyNotNull($"LevelManager is not set for FinishTrigger instance. GameObject name = {name}")
                        .InvokeLevelCompleted();
                }
            }
        }
    }
}
