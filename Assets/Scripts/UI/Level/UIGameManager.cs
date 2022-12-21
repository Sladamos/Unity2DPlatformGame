using System;
using MIIProjekt.Logging;
using MIIProjekt.Player;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class UIGameManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private PlayerLifeDisplay playerLifeDisplay;
        private PlayerScoreDisplay playerScoreDisplay;

        [SerializeField]
        private PlayerLife playerLife;

        [SerializeField]
        private PlayerScore playerScore;

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            if (playerLife != null)
            {
                playerLife.PlayerLifeChanged += OnPlayerLifeChanged;
            }
            else
            {
                Logger.Warn("PlayerLife instance is not set. The Game UI will not be notified of changes in player's life.");
            }

            if (playerScore != null)
            {
                playerScore.PlayerScoreChanged += OnPlayerScoreChanged;
            }
            else
            {
                Logger.Warn("PlayerScore instance is not set. The Game UI will not be notified of changes in player's score.");
            }

            playerLifeDisplay = GetComponentInChildren<PlayerLifeDisplay>();
            if (playerLifeDisplay != null)
            {
                int? optionalLives = playerLife?.Lives;
                playerLifeDisplay.DisplayHearts(optionalLives.GetValueOrDefault());
            }
            else
            {
                Logger.Error("PlayerLifeDisplay dependency not found. Game object name = {}", name);
            }

            playerScoreDisplay = GetComponentInChildren<PlayerScoreDisplay>();
            if (playerScoreDisplay != null)
            {
                int? optionalScore = playerScore?.Score;
                playerScoreDisplay.UpdateScore(optionalScore.GetValueOrDefault());
            }
            else
            {
                Logger.Error("PlayerScoreDisplay dependency not found. Game object name = {}", name);
            }
        }

        private void OnPlayerLifeChanged(int oldValue, int newValue)
        {
            playerLifeDisplay?.DisplayHearts(newValue);
        }

        private void OnPlayerScoreChanged(int oldValue, int newValue)
        {
            playerScoreDisplay?.UpdateScore(newValue);
        }
    }
}
