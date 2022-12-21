using MIIProjekt.Logging;
using NLog;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.UI.Level
{
    public class GameOverComponent : MonoBehaviour
    {
        private readonly static NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnMainMenuClicked()
        {
            Logger.Debug("Clicked MainMenu");
            SceneManager.LoadScene("MainMenu");
        }

        public void OnRestartClicked()
        {
            Logger.Debug("Clicked Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }
    }
}
