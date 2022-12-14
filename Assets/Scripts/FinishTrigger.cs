using UnityEngine;
using MIIProjekt.Player;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour
    {
        private bool nextLevelIsUnlocked = false;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                CollisionWithPlayer(collider);
            }
        }

        private void CollisionWithPlayer(Collider2D player)
        {
            if (nextLevelIsUnlocked)
            {
                //player.GetComponent<PlayerScore>().
                player.SendMessage("Finish");
                GameManagers.GameManager.instance.LevelCompleted();
            }
            else
            {
                Debug.Log("Zbierz wszystkie klucze!");
            }
        }

        private void OnAllKeysCollected()
        {
            Debug.Log("Udaj sie do wyjscia!");
            nextLevelIsUnlocked = true;
        }
    }
}
