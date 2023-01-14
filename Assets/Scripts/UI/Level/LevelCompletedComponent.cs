using MIIProjekt.Extensions;
using MIIProjekt.Logging;
using MIIProjekt.Player;
using NLog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using MIIProjekt.GameManagers;

namespace MIIProjekt.UI.Level
{
    public class LevelCompletedComponent : MonoBehaviour
    {
        private readonly static NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private PlayerScore playerScore;

        [SerializeField]
        private HighScoresManager highScoreManager;

        [SerializeField]
        private TMP_Text scoreLabel;

        [SerializeField]
        private TMP_Text highScoreLabel;

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

        private void OnEnable()
        {
            scoreLabel.VerifyNotNull().SetText(playerScore.Score.ToString("0000"));
            highScoreLabel.VerifyNotNull().SetText(highScoreManager.HighScore.ToString("0000"));
        }
    }
}
