using MIIProjekt.Logging;
using NLog;
using TMPro;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class PlayerScoreDisplay : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private TMP_Text text;

        public void UpdateScore(int value)
        {
            if (text == null)
            {
                return;
            }
        
            string formattedString = value.ToString("000");
            text.SetText(formattedString);
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (text == null)
            {
                Logger.Warn("PlayerScoreDisplay instance does not have the text object set. Updating the score will have no effect. GameObject name = {}", name);
            }
        }
    }
}
