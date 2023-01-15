using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateOnGround : PlayerState
    {
        public PlayerStateOnGround(PlayerController2 playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            ChangeAnimation("OnGround");
            Controller.InvokePlayerLanded();
        }

        public override void Process()
        {
            if (Input.GetButtonDown("Jump"))
            {
                InvokeTransition(PlayerTransition.Jumped);
            }
            
            if (Input.GetButtonDown("Dash"))
            {
                InvokeTransition(PlayerTransition.EnterDash);
            }
        }

        public override void PhysicsProcess()
        {
            AddGravityAndLimitFallingSpeed();
            MovePlayer();

            Controller.Animator.SetFloat("VelocityX", Mathf.Abs(Velocity.x));
        }
    }
}
