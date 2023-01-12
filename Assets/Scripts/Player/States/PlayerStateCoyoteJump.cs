using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateCoyoteJump : PlayerState
    {
        private float timeLeft;

        public PlayerStateCoyoteJump(PlayerController2 playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            timeLeft = Controller.CoyoteTime;
        }

        public override void Process()
        {
            if (Input.GetButtonDown("Jump"))
            {
                InvokeTransition(PlayerTransition.Jumped);
            }

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) 
            {
                InvokeTransition(PlayerTransition.CoyoteTimeFinished);
            }
        }

        public override void PhysicsProcess()
        {
            AddGravityAndLimitFallingSpeed();
            MovePlayer();
        }
    }
}
