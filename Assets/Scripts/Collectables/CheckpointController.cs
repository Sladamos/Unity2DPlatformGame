using System.Collections;
using System.Collections.Generic;
using NLog;
using UnityEngine;

namespace MIIProjekt.Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckpointController : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        private Collider2D selfCollider;

        private void Awake()
        {
            selfCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Logger.Debug("Checkpoint reached");
                Vector2 newSpawnPoint = transform.position;
                collider.SendMessage("SetSpawnPoint", newSpawnPoint);
                selfCollider.enabled = false;
            }
        }
    }
}
