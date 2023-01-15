using NLog;
using UnityEngine;

namespace MIIProjekt.Player.States
{
    public class PlayerStateDash : PlayerState
    {
        private const float PressThreshold = 0.5f;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private Vector2 dashDirection;
        private float timeLeft;

        public PlayerStateDash(PlayerController2 playerController) : base(playerController)
        {

        }

        public override void EnterState()
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dashDirection = DirectionFromInput(input);

            Logger.Debug("Dashed! Input: {}, dashDirection: {}", input, dashDirection);

            timeLeft = Controller.DashTime;
        }

        public override void Process()
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                if (Controller.IsOnGround)
                {
                    InvokeTransition(PlayerTransition.DashFinishedOnGround);
                }
                else
                {
                    InvokeTransition(PlayerTransition.DashFinishedNotOnGround);
                }
            }
        }

        public override void PhysicsProcess()
        {
            Velocity = dashDirection * Controller.InitialDashSpeed;
        }

        private Vector2 DirectionFromInput(Vector2 input)
        {
            bool isLeftPressed = input.x < -PressThreshold;
            bool isRightPressed = input.x > PressThreshold;
            bool isDownPressed = input.y < -PressThreshold;
            bool isUpPressed = input.y > PressThreshold;

            Vector2 direction = new Vector2(
                x: (isLeftPressed ? -1.0f : 0.0f) + (isRightPressed ? 1.0f : 0.0f),
                y: (isDownPressed ? -1.0f : 0.0f) + (isUpPressed ? 1.0f : 0.0f)
            );
            
            if (direction.x != 0.0f || direction.y != 0.0f)
            {
                return direction;
            }

            return new Vector2(Controller.IsFacingRight ? 1.0f : -1.0f, 0.0f);
        }
    }
}
