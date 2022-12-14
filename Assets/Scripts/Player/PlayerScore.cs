using MIIProjekt.GameManagers;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerScore : MonoBehaviour
    {
        private int score = 0;

        void Start()
        {
            DisplayManager.instance.SendMessage("SetScore", score);
        }

        private void IncreaseScore(int value)
        {
            score += value;
            DisplayManager.instance.SendMessage("SetScore", score);
        }
        private void MultiplyScoreByLives(int multiplicator)
        {
            IncreaseScore(multiplicator * GetComponent<PlayerLife>().Lives);
        }
    }
}
