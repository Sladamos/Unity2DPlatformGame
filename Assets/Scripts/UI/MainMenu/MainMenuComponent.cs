using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnClickedButtonLevel1()
        {
            Logger.Debug("On clicked level 1");
            SceneManager.LoadScene("Level1");
        }
        
        public void OnClickedButtonLevel2()
        {
            Logger.Debug("On clicked level 2");
        }

        public void OnClickedButtonExitGame()
        {
            Logger.Debug("Exit game");
            Application.Quit();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
