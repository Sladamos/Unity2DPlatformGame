using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MIIProjekt.Extensions;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour
    {
        private bool nextLevelIsUnlocked = false;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                collisionWithPlayer(collider);
            }
        }

        private void collisionWithPlayer(Collider2D player)
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
            Debug.Log("Udaj siê do wyjœcia!");
            nextLevelIsUnlocked = true;
        }
    }
}
