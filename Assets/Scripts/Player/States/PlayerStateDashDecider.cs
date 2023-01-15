using NLog;
using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateDashDecider : PlayerState
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private bool playerLandedSinceLastDash = true;

        public PlayerStateDashDecider(PlayerController2 playerController) : base(playerController)
        {
            Controller.PlayerLanded += OnPlayerLanded;
        }

        public override void EnterState()
        {
            if (CanEnterDash())
            {
                playerLandedSinceLastDash = false;
                InvokeTransition(PlayerTransition.DashSuccess);
                return;
            }
            
            if (Controller.IsOnGround)
            {
                InvokeTransition(PlayerTransition.DashFailedOnGround);
            }
            else
            {
                InvokeTransition(PlayerTransition.DashFailedNotOnGround);
            }
        }
        
        private bool CanEnterDash()
        {
            return playerLandedSinceLastDash;
        }

        private void OnPlayerLanded(Vector2 landPosition)
        {
            playerLandedSinceLastDash = true;
        }
    }
}
