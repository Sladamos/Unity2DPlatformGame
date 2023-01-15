using System.Collections;
using System.Collections.Generic;
using MIIProjekt.Player;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class SpikesCollider : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                var playerLife = collider.GetComponent<PlayerLife>();
                if (playerLife == null)
                {
                    Logger.Warn("No PlayerLife instance found on object {}", playerLife.name);
                    return;
                }

                playerLife.GetHit();
            }
        }
    }

}
