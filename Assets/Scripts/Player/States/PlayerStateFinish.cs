using NLog;
using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateFinish : PlayerState
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public PlayerStateFinish(PlayerController2 playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            Logger.Info("Player finished!");
            Velocity = Vector2.zero;
            ChangeAnimation("OnGround");
        }
    }
}
