using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateFalling : PlayerState
    {
        public PlayerStateFalling(PlayerController2 playerController) : base(playerController)
        {

        }

        public override void EnterState()
        {
            ChangeAnimation("InAir");
        }

        public override void PhysicsProcess()
        {
            AddGravityAndLimitFallingSpeed();
            MovePlayer();
        }
    }
}
