using System;
using MIIProjekt.Collectables;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerScore : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action<int, int> PlayerScoreChanged;

        [SerializeField]
        private FinishComponent finishComponent;

        private int score = 0;
        private int scoreForLive = 100;

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

        public void OnEndGameCollectableCollected(ICollectable collectable)
        {
            Score += collectable.Score;
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (finishComponent != null)
            {
                finishComponent.EndGameCollectableCollected += OnEndGameCollectableCollected;
            }
        }

        private void Start()
        {
            Score = 0;
        }

        private void CalculateFinalScore()
        {
            PlayerLife playerLife = this.GetComponent<PlayerLife>();
            Score += playerLife.Lives * scoreForLive;
        }
    }
}
