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
            ChangeAnimation("Fall");
        }

        public override void Process()
        {
            if (Input.GetButtonDown("Dash"))
            {
                InvokeTransition(PlayerTransition.EnterDash);
            }
        }

        public override void PhysicsProcess()
        {
            AddGravityAndLimitFallingSpeed();
            MovePlayer();
        }
    }
}
