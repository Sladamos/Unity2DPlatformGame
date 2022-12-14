using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text endScoreText;

        private void SetScore(int score)
        {
            scoreText.text = score.ToString("000");
            endScoreText.text = "Your score: " + score.ToString("0000");
        }
    }
}
