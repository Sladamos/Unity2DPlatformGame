using System.Collections.Generic;
using MIIProjekt.UI.Level;
using NLog;
using UnityEngine;

namespace MIIProjekt
{
    public class CheckpointManager : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<string> VisitedCheckpoints { get; }

        [SerializeField]
        private CheckpointMessageDisplayer checkpointMessageDisplayer;

        [SerializeField]
        private PlayerRespawn playerRespawn;

        public CheckpointManager()
        {
            VisitedCheckpoints = new();
        }

        public void SetCheckpoint(string checkpointName, Vector2 checkpointPosition)
        {
            // Do not display text twice for the same checkpoint
            bool shouldDisplayText = !VisitedCheckpoints.Contains(checkpointName);
            
            Logger.Debug("Setting checkpoint {} to {}. Should display text = {}", checkpointName, checkpointPosition, shouldDisplayText);
            
            if (shouldDisplayText)
            {
                checkpointMessageDisplayer?.Display();
            }

            playerRespawn?.SetSpawnPoint(checkpointPosition);
        }

        private void Awake()
        {
            if (checkpointMessageDisplayer == null)
            {
                Logger.Warn("CheckpointMessageDisplayer is null. No checkpoint will be printed to screen.");
            }

            if (playerRespawn == null)
            {
                Logger.Warn("PlayerRespawn is null. No checkpoint will be active.");
            }
        }
    }
}
