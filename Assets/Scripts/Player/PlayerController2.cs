using System.Collections.Generic;
using MIIProjekt.Player.States;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2 : MonoBehaviour
    {
        private const float UpVectorAngle = Mathf.PI / 2f;
        private const float AngleEpsilon = Mathf.PI / 4f;

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        private Dictionary<PlayerStateEnum, Dictionary<PlayerTransition, PlayerStateEnum>> Transitions { get; }
        private Dictionary<PlayerStateEnum, PlayerState> StateMap { get; }

        private new Rigidbody2D rigidbody2D;

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
        private float playerColliderWidth;

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

        private List<Vector2> collisionPositions = new List<Vector2>();

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

                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x);
                float angleDifference = Mathf.Abs(angle - UpVectorAngle);
                Logger.Debug("Angle: {}, difference = {}, angleEpsilon = {} position = {}", angle, angleDifference, AngleEpsilon, contact.point);
                if (angleDifference > AngleEpsilon)
                {
                    continue;
                }

                collisionPositions.Add(contact.point);
                
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

        private void OnGizmosDraw()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(playerColliderWidth, 0.1f, 0.0f));
            foreach (Vector2 vector in collisionPositions)
            {
                Vector3 vector3 = new Vector3(vector.x, vector.y, -20f);
                Gizmos.DrawSphere(vector3, 1f);                
            }
            collisionPositions.Clear();
        }
    }
}
