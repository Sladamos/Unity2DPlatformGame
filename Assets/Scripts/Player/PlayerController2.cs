using System;
using System.Collections.Generic;
using MIIProjekt.Player.States;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController2 : MonoBehaviour
    {
        private const float UpVectorAngle = Mathf.PI / 2f;
        private const float AngleEpsilon = Mathf.PI / 4f;
        private const float FlipThreshold = 0.1f;

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action PlayerJumped;
        public event Action<Vector2> PlayerLanded;

        private Dictionary<PlayerStateEnum, Dictionary<PlayerTransition, PlayerStateEnum>> Transitions { get; }
        private Dictionary<PlayerTransition, PlayerStateEnum> DefaultTransitions { get; }
        private Dictionary<PlayerStateEnum, PlayerState> StateMap { get; }

        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody2D;
        public Animator animator;

        private bool lastFrameIsOnGround = false;
        private bool isOnGround;

        [Header("Debug")]
        [SerializeField]
        private PlayerStateEnum playerState = PlayerStateEnum.Falling;

        [Header("Moving")]
        [SerializeField]
        private float moveSpeed = 10;

        [Header("Falling")]
        [SerializeField]
        private float gravity = 70;

        [SerializeField]
        private float maxFallingSpeed = 40;

        [SerializeField]
        private float coyoteTime = 1.0f;

        [Header("Jumping")]
        [SerializeField]
        private float minJumpTime = 0.2f;

        [SerializeField]
        private float maxJumpTime = 0.5f;

        [SerializeField]
        private float jumpForce = 35;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float gravityEffectTimePercent = 0.95f;

        public float Gravity => gravity;
        public float MaxFallingSpeed => maxFallingSpeed;
        public float MinJumpTime => minJumpTime;
        public float MaxJumpTime => maxJumpTime;
        public float JumpForce => jumpForce;
        public bool IsOnGround => isOnGround;
        public float GravityEffectTimePercent => gravityEffectTimePercent;
        public float MoveSpeed => moveSpeed;
        public float CoyoteTime => coyoteTime;

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
            transitionsOnGround.Add(PlayerTransition.Jumped, PlayerStateEnum.Jumping);
            transitionsOnGround.Add(PlayerTransition.PlayerNotOnGround, PlayerStateEnum.CoyoteJump);

            Dictionary<PlayerTransition, PlayerStateEnum> transitionsCoyoteJump = new();
            transitionsCoyoteJump.Add(PlayerTransition.CoyoteTimeFinished, PlayerStateEnum.Falling);
            transitionsCoyoteJump.Add(PlayerTransition.Jumped, PlayerStateEnum.Jumping);
            transitionsCoyoteJump.Add(PlayerTransition.PlayerOnGround, PlayerStateEnum.OnGround);

            DefaultTransitions = new();
            DefaultTransitions.Add(PlayerTransition.Died, PlayerStateEnum.Dead);
            DefaultTransitions.Add(PlayerTransition.Finish, PlayerStateEnum.Finish);

            Transitions = new();
            Transitions.Add(PlayerStateEnum.Falling, transitionsFalling);
            Transitions.Add(PlayerStateEnum.Jumping, transitionsJumping);
            Transitions.Add(PlayerStateEnum.OnGround, transitionsOnGround);
            Transitions.Add(PlayerStateEnum.CoyoteJump, transitionsCoyoteJump);
            Transitions.Add(PlayerStateEnum.Dead, new());

            StateMap = new();
            playerState = PlayerStateEnum.Falling;
        }

        public void InvokeTransition(PlayerTransition transition)
        {
            Logger.Debug("Transition: {}", transition);
            Dictionary<PlayerTransition, PlayerStateEnum> currentStateTransitions = Transitions.GetValueOrDefault(playerState);
            if (currentStateTransitions == null)
            {
                currentStateTransitions = DefaultTransitions;
                return;
            }

            PlayerStateEnum nextState = currentStateTransitions.GetValueOrDefault(transition, PlayerStateEnum.Invalid);

            if (nextState == PlayerStateEnum.Invalid)
            {
                nextState = DefaultTransitions.GetValueOrDefault(transition, PlayerStateEnum.Invalid);
                if (nextState == PlayerStateEnum.Invalid)
                {
                    Logger.Debug("No next state for transition {}", transition);
                    return;
                }
            }

            ChangeState((PlayerStateEnum)nextState);
        }

        public void InvokePlayerJumped()
        {
            PlayerJumped?.Invoke();
        }

        public void InvokePlayerLanded()
        {
            PlayerLanded?.Invoke(transform.position);
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
            StateMap.Add(PlayerStateEnum.Jumping, new PlayerStateJumping(this));
            StateMap.Add(PlayerStateEnum.Dead, new PlayerStateDead(this));
            StateMap.Add(PlayerStateEnum.Finish, new PlayerStateFinish(this));
            StateMap.Add(PlayerStateEnum.CoyoteJump, new PlayerStateCoyoteJump(this));
            
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            StateMap[playerState].Process();

            if (Velocity.x > FlipThreshold)
            {
                spriteRenderer.flipX = false;
            }
            else if (Velocity.x < -FlipThreshold)
            {
                spriteRenderer.flipX = true;
            }
        }

        private void FixedUpdate()
        {
            StateMap[playerState].PhysicsProcess();

            if (lastFrameIsOnGround)
            {
                if (!isOnGround)
                {
                    InvokeTransition(PlayerTransition.PlayerNotOnGround);
                }
            }
            else
            {
                if (isOnGround)
                {
                    InvokeTransition(PlayerTransition.PlayerOnGround);
                }
            }

            lastFrameIsOnGround = isOnGround;
            isOnGround = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint2D contact = collision.GetContact(i);

                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x);
                float angleDifference = Mathf.Abs(angle - UpVectorAngle);
                if (angleDifference > AngleEpsilon)
                {
                    continue;
                }

                if (contact.point.y < transform.position.y)
                {
                    isOnGround = true;
                    break;
                }
            }
        }

        private void PlayerDied()
        {
            InvokeTransition(PlayerTransition.Died);
        }

        private void Finish()
        {
            this.SendMessage("CalculateFinalScore");
            InvokeTransition(PlayerTransition.Finish);
        }
    }
}
