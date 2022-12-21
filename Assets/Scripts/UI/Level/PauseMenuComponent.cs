using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.Level
{
    public class PauseMenuComponent : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private UIAnimatorController uiAnimatorController;

        public void OnClickedButtonResume()
        {
            Logger.Debug("Clicked button resume");
            uiAnimatorController?.InvokeGameResumeTrigger();
        }

        public void OnClickedButtonRestart()
        {
            Logger.Debug("Clicked button restart");
            SceneManager.LoadScene((SceneManager.GetActiveScene().name));
        }

        public void OnClickedButtonOptions()
        {
            Logger.Debug("Clicked button options");
            uiAnimatorController?.InvokeOptionsTrigger();
        }

        public void OnClickedButtonMainMenu()
        {
            Logger.Debug("Clicked button main menu");
            SceneManager.LoadScene("MainMenu");
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (uiAnimatorController == null)
            {
                Logger.Warn("UIAnimatorController is not set on PauseMenuComponent instance. GameObject name = {}", name);
            }
        }
    }
}
