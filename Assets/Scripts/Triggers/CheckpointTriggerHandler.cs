using NLog;
using UnityEngine;

namespace MIIProjekt.Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckpointTriggerHandler : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [SerializeField]
        private string checkpointName;

        [SerializeField]
        private Vector2 checkpointPosition;

        public void OnTrigger(Collider2D collider)
        {
            Logger.Debug("CheckpointTriggerHandler: OnTrigger");
            var manager = GetComponentInParent<CheckpointManager>();
            if (manager == null)
            {
                Logger.Error("No CheckpointManager found for CheckpointTriggerHandler instance. Name = {}", name);
                return;
            }

            manager.SetCheckpoint(checkpointName, checkpointPosition);
        }

        private void OnDrawGizmos()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.blue;
            
            Gizmos.DrawSphere(checkpointPosition, 0.5f);
            
            Gizmos.color = oldColor;
        }
    }
}
