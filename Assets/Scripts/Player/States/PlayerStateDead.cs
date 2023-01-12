using NLog;
using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateDead : PlayerState
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public PlayerStateDead(PlayerController2 playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            Logger.Info("Player is dead!");
            Velocity = Vector2.zero;
            ChangeAnimation("Dead");
        }
    }
}
