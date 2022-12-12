using UnityEngine;

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
                player.SendMessage("Finish");
            }
            else
            {
                Debug.Log("Zbierz wszystkie klucze!");
            }
        }

        private void OnAllKeysCollected()
        {
            Debug.Log("Udaj si� do wyj�cia!");
            nextLevelIsUnlocked = true;
        }
    }
}
