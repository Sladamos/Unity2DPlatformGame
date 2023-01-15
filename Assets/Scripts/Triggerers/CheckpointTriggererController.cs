using NLog;
using UnityEngine;
using UnityEngine.Events;

namespace MIIProjekt.Triggerers
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckpointTriggererController : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        public UnityEvent<Vector2> checkpointReached;

        private Collider2D selfCollider;

        private void Awake()
        {
            selfCollider = GetComponent<Collider2D>();
        }

        private void SetCheckpoint(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Logger.Debug("Checkpoint reached");
                Vector2 newSpawnPoint = transform.position;
                checkpointReached.Invoke(newSpawnPoint);
                selfCollider.enabled = false;
            }
        }
    }
}
