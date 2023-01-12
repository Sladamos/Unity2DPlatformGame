using NLog;
using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateJumping : PlayerState
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private float timeLeft;
        private bool isJumpingPressed;

        private float MinRequired => Controller.MaxJumpTime - Controller.MinJumpTime;

        public PlayerStateJumping(PlayerController2 playerController) : base(playerController)
        {
        }

        public override void EnterState()
        {
            timeLeft = Controller.MaxJumpTime;
            isJumpingPressed = true;
        }

        public override void Process()
        {
            timeLeft -= Time.deltaTime;
            isJumpingPressed = Input.GetButton("Jump");
        }

        public override void PhysicsProcess()
        {
            MovePlayer();

            if (!ShouldJumpingContinue())
            {
                InvokeTransition(PlayerTransition.JumpingFinished);
                return;
            }

            float percent = 1.0f - Mathf.Clamp(timeLeft / Controller.MaxJumpTime, 0.0f, 1.0f);
            float progress = Mathf.Max(0.0f, -Mathf.Sqrt(percent) + Controller.GravityEffectTimePercent);
            float verticalVelocity = progress * Controller.JumpForce;

            // Replace old y velocity
            Vector2 currentVelocity = Controller.Velocity;
            currentVelocity.y = verticalVelocity;
            Controller.Velocity = currentVelocity;
        }

        public override void ExitState()
        {
            Logger.Debug("Jumped for {} seconds", Controller.MaxJumpTime - timeLeft);
        }
        
        private bool ShouldJumpingContinue()
        {
            if (timeLeft > MinRequired)
            {
                return true;
            }

            if (!isJumpingPressed)
            {
                return false;
            }

            if (timeLeft < 0)
            {
                return false;
            }

            return true;
        }
    }
}
