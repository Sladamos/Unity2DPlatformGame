using System.Collections.Generic;
using MIIProjekt.Player.States;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2 : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        private Dictionary<PlayerStateEnum, Dictionary<PlayerTransition, PlayerStateEnum>> Transitions { get; }
        private Dictionary<PlayerStateEnum, PlayerState> StateMap { get; }

        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float gravity = 50;

        [SerializeField]
        private float maxFallingSpeed = 20;

        [SerializeField]
        private PlayerStateEnum playerState;

        [SerializeField]
        private float minJumpTime;

        [SerializeField]
        private float maxJumpTime;
        
        [SerializeField]
        private float jumpForce;

        public float Gravity => gravity;
        public float MaxFallingSpeed => maxFallingSpeed;
        public float MinJumpTime => minJumpTime;
        public float MaxJumpTime => maxJumpTime;
        public float JumpForce => jumpForce;

        public Vector2 Velocity
        {
            get
            {
                return rigidbody2D.velocity;
            }
            set
            {
                rigidbody2D.velocity = value;
            }
        }

        public PlayerController2()
        {
            Dictionary<PlayerTransition, PlayerStateEnum> transitionsFalling = new();
            transitionsFalling.Add(PlayerTransition.PlayerOnGround, PlayerStateEnum.OnGround);

            Dictionary<PlayerTransition, PlayerStateEnum> transitionsJumping = new();
            transitionsJumping.Add(PlayerTransition.JumpingFinished, PlayerStateEnum.Falling);

            Dictionary<PlayerTransition, PlayerStateEnum> transitionsOnGround = new();

            Transitions = new();
            Transitions.Add(PlayerStateEnum.Falling, transitionsFalling);
            Transitions.Add(PlayerStateEnum.Jumping, transitionsJumping);
            Transitions.Add(PlayerStateEnum.OnGround, transitionsOnGround);

            StateMap = new();
            playerState = PlayerStateEnum.Falling;
        }

        public void InvokeTransition(PlayerTransition transition)
        {
            Dictionary<PlayerTransition, PlayerStateEnum> currentStateTransitions = Transitions.GetValueOrDefault(playerState);
            if (currentStateTransitions == null)
            {
                Logger.Error("No transitions for state {}", playerState);
                return;
            }

            PlayerStateEnum? nextState = currentStateTransitions.GetValueOrDefault(transition);

            if (nextState == null)
            {
                Logger.Debug("No next state for transition {}", transition);
                return;
            }

            ChangeState((PlayerStateEnum)nextState);
        }

        private void ChangeState(PlayerStateEnum playerState)
        {
            if (this.playerState == playerState)
            {
                return;
            }

            Logger.Trace("Changed player state {} -> {}", this.playerState, playerState);

            StateMap[this.playerState].ExitState();
            this.playerState = playerState;
            StateMap[playerState].EnterState();
        }

        private void Awake()
        {
            StateMap.Add(PlayerStateEnum.OnGround, new PlayerStateOnGround(this));
            StateMap.Add(PlayerStateEnum.Falling, new PlayerStateFalling(this));
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            StateMap[playerState].Process();
        }

        private void FixedUpdate()
        {
            StateMap[playerState].PhysicsProcess();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            bool groundCollide = false;
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint2D contact = collision.GetContact(i);
                if (contact.point.y < transform.position.y)
                {
                    groundCollide = true;
                    break;
                }
            }

            if (groundCollide)
            {
                InvokeTransition(PlayerTransition.PlayerOnGround);
            }
        }
    }
}
