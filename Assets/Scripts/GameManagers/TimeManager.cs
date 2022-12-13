using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class TimeManager : MonoBehaviour
    {
        private void UpdateTime()
        {
            if (GameManager.instance.IsGameCurrentlyPlayed())
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
}
