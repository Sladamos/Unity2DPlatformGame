using UnityEngine;

namespace MIIProjekt.Player.States
{
    public abstract class PlayerState
    {
        protected PlayerController2 Controller { get; }
        protected Transform Transform { get; } 

        protected Vector2 Velocity
        {
            get
            {
                return Controller.Velocity;
            }
            set
            {
                Controller.Velocity = value;
            }
        }

        protected PlayerState(PlayerController2 playerController)
        {
            this.Controller = playerController;
            this.Transform = playerController.transform;
        }

        public virtual void EnterState() { }
        public virtual void Process() { }
        public virtual void PhysicsProcess() { }
        public virtual void ExitState() { }

        public void InvokeTransition(PlayerTransition transition)
        {
            Controller.InvokeTransition(transition);
        }

        protected void ChangeAnimation(string animation)
        {
            Controller.animator.SetTrigger(animation);
        }

        protected void AddGravityAndLimitFallingSpeed()
        {
            Vector2 velocity = Controller.Velocity;
            velocity += Vector2.down * Controller.Gravity * Time.fixedDeltaTime;
            velocity.y = Mathf.Max(-Controller.MaxFallingSpeed, velocity.y);
            Velocity = velocity;
        }

        protected void MovePlayer()
        {
            float input = Input.GetAxis("Horizontal");

            float speed = Controller.MoveSpeed;
            Vector2 velocity = Controller.Velocity;

            velocity.x = input * speed;

            Controller.Velocity = velocity;
        }
    }
}
