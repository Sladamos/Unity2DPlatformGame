using System;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.Level
{
    public class PauseMenuComponent : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnClickedButtonResume()
        {
            Logger.Debug("Clicked button resume");
            throw new NotImplementedException();
        }

        public void OnClickedButtonRestart()
        {
            Logger.Debug("Clicked button restart");
            SceneManager.LoadScene((SceneManager.GetActiveScene().name));
        }

        public void OnClickedButtonMainMenu()
        {
            Logger.Debug("Clicked button main menu");
            SceneManager.LoadScene("MainMenu");
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
