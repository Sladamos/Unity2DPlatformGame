using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.MainMenu
{
    public class MainMenuComponent : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private GameObject options;

        [SerializeField]
        private GameObject about;

        public void OnClickedButtonLevel1()
        {
            Logger.Debug("On clicked level 1");
            SceneManager.LoadScene("Level1");
        }

        public void OnClickedButtonOptions()
        {
            this.gameObject.SetActive(false);
            options.SetActive(true);
        }

        public void OnClickedButtonAbout()
        {
            this.gameObject.SetActive(false);
            about.SetActive(true);
        }

        public void OnClickedButtonExitGame()
        {
            Logger.Debug("Exit game");
            Application.Quit();
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
            //Initialize awake
            options.SetActive(true);
            options.SetActive(false);
        }
    }
}
