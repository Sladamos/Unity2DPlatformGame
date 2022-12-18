using System;
using MIIProjekt.GameManagers;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerScore : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action<int, int> PlayerScoreChanged;

        private int score = 0;

        public int Score
        {
            get => score;
            private set
            {
                int oldValue = score;

                score = value;

                Logger.Debug("Changed player score: {} -> {}", oldValue, value);
                PlayerScoreChanged?.Invoke(oldValue, value);
            }
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();
        }

        void Start()
        {
            DisplayManager.instance.SendMessage("SetScore", Score);
        }

        private void IncreaseScore(int value)
        {
            Score += value;
            DisplayManager.instance.SendMessage("SetScore", Score);
        }

        private void MultiplyScoreByLives(int multiplicator)
        {
            IncreaseScore(multiplicator * GetComponent<PlayerLife>().Lives);
        }
    }
}
