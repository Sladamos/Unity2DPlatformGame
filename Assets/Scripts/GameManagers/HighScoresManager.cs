using System.Collections;
using System.Collections.Generic;
using MIIProjekt.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.GameManagers 
{
    public class HighScoresManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerScore playerScore;

        private string keyHighScore;

        public int HighScore
        {
            get
            {
                return PlayerPrefs.GetInt(keyHighScore);
            }
            private set
            {
                int oldValue = HighScore;
                if (value > HighScore)
                {
                    PlayerPrefs.SetInt(keyHighScore, value);
                }
            }
        }
        private void Awake()
        {
            playerScore.PlayerScoreChanged += OnScoreChange;
            keyHighScore = SceneManager.GetActiveScene().name;
            if (!PlayerPrefs.HasKey(keyHighScore))
            {
                PlayerPrefs.SetInt(keyHighScore, 0);
            }
        }

        private void OnScoreChange(int oldValue, int newValue)
        {
            HighScore = newValue;
        }
    }
}
