using System;
using MIIProjekt.Extensions;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    [RequireComponent(typeof(TimeManager))]
    public class LevelManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action LevelCompleted;
        public event Action GameOver;

        private TimeManager timeManager;

        public void InvokeLevelCompleted()
        {
            Logger.Debug("Invoking LevelCompleted event");
            LevelCompleted?.Invoke();
        }

        public void InvokeGameOver()
        {
            Logger.Debug("Invoking GameOver event");
            GameOver?.Invoke();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            timeManager = GetComponent<TimeManager>().VerifyNotNull($"TimeManager is required for LevelManager. GameObject instance's name = {name}");
        }

        private void Update()
        {
            // TODO: This is temporary. Move this code somewhere else.
            if (!timeManager.IsGamePaused()) {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Logger.Debug("Detected pause game request");
                    timeManager.PauseGame();
                }
            }
        }
    }
}
