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
        private Dictionary<PlayerStateEnum, Dictionary<PlayerTransition, PlayerStateEnum>> Transitions { get; }
        private Dictionary<PlayerStateEnum, PlayerState> StateMap { get; }

        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody2D;
        public Animator animator;

        private bool isOnGround;

        [Header("Debug")]
        [SerializeField]
        private PlayerStateEnum playerState;

        [Header("Moving")]
        [SerializeField]
        private float moveSpeed;

        [Header("Falling")]
        [SerializeField]
        private float gravity = 50;

        [SerializeField]
        private float maxFallingSpeed = 20;

        [SerializeField]
        private float coyoteTime = 1.0f;

        [Header("Jumping")]
        [SerializeField]
        private float minJumpTime;

        [SerializeField]
        private float maxJumpTime;
        
        [SerializeField]
        private float jumpForce;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float gravityEffectTimePercent = 0.92f;

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

            Transitions = new();
            Transitions.Add(PlayerStateEnum.Falling, transitionsFalling);
            Transitions.Add(PlayerStateEnum.Jumping, transitionsJumping);
            Transitions.Add(PlayerStateEnum.OnGround, transitionsOnGround);
            Transitions.Add(PlayerStateEnum.CoyoteJump, transitionsCoyoteJump);

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

            PlayerStateEnum nextState = currentStateTransitions.GetValueOrDefault(transition, PlayerStateEnum.Invalid);

            if (nextState == PlayerStateEnum.Invalid)
            {
                Logger.Debug("No next state for transition {}", transition);
                return;
            }

            Logger.Debug("Transition: {}, nextstate: {}", transition, nextState);
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
            StateMap.Add(PlayerStateEnum.Jumping, new PlayerStateJumping(this));
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
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            bool groundCollide = false;
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
                    groundCollide = true;
                    break;
                }
            }

            isOnGround = true;
            
            if (groundCollide)
            {
                InvokeTransition(PlayerTransition.PlayerOnGround);
            }
        }
    }
}
