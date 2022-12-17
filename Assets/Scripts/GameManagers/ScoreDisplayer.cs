using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIIProjekt.GameManagers
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text endScoreText;

        [SerializeField]
        private TMP_Text highScoreText;

        private string keyHighScore = "HighScoreLevel1";

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(keyHighScore))
            {
                PlayerPrefs.SetInt(keyHighScore, 0);
            }
        }

        private void SetScore(int score)
        {
            scoreText.text = score.ToString("000");
            endScoreText.text = "Your score: " + score.ToString("0000");
            CheckHighestScore(score);
        }

        private void CheckHighestScore(int score)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level1")
            {
                int highScore = PlayerPrefs.GetInt(keyHighScore);
                if (highScore < score)
                {
                    highScore = score;
                    PlayerPrefs.SetInt(keyHighScore, score);
                }
                highScoreText.text = "Highest score: " + highScore.ToString("0000");
            }
        }
    }
}
