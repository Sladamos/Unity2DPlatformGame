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
        private Dictionary<PlayerStateEnum, PlayerState> StateMap { get; }

        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float gravity = 50;

        [SerializeField]
        private float maxFallingSpeed = 20;

        [SerializeField]
        private PlayerStateEnum playerState;

        public float Gravity => gravity;
        public float MaxFallingSpeed => maxFallingSpeed;

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
            StateMap = new();
            playerState = PlayerStateEnum.Falling;
        }

        public void ChangeState(PlayerStateEnum playerState)
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
    }
}
