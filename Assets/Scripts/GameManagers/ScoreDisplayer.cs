using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;

        private void SetScore(int score)
        {
            scoreText.text = score.ToString("000");
        }
    }
}
