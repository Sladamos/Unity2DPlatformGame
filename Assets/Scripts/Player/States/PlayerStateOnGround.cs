using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateOnGround : PlayerState
    {
        public PlayerStateOnGround(PlayerController2 playerController) : base(playerController)
        {
            
        }

        public override void Process()
        {
            if (Input.GetButtonDown("Jump"))
            {
                InvokeTransition(PlayerTransition.Jumped);
            }
        }

        public override void PhysicsProcess()
        {
            AddGravityAndLimitFallingSpeed();
        }
    }
}
