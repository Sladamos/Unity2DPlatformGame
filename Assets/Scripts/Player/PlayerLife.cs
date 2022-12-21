using System;
using MIIProjekt.GameManagers;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerLife : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action<int, int> PlayerLifeChanged;

        private Rigidbody2D playerRigidbody;
        private PlayerController playerController;
        private Animator animator;
        private int lives = 0;

        [SerializeField]
        private int startingLives = 3;

        [SerializeField]
        private int maximumLives = 5;

        [SerializeField]
        private LevelManager levelManager;

        public int Lives
        {
            get => lives;
            private set
            {
                int oldValue = lives;

                lives = value;
                
                Logger.Debug("Changed number of lives: {} -> {}", oldValue, value);
                PlayerLifeChanged?.Invoke(oldValue, value);
            }
        }

        public bool CanPickupBonusLife()
        {
            return lives < maximumLives;
        }

        private void GetHit()
        {
            DecreaseLives(1);
        }

        private void Death()
        {
            playerRigidbody.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("death");
            playerController.enabled = false;

            levelManager?.InvokeGameOver();
        }

        private void IncreaseLives(int numberOfLives)
        {
            if (numberOfLives + lives < maximumLives)
            {
                Lives += numberOfLives;
            }
            else
            {
                Lives = maximumLives;
            }
        }

        private void DecreaseLives(int numberOfLives)
        {
            Lives -= numberOfLives;
            
            if (Lives > 0)
            {
                this.SendMessage("ReturnToSpawn");
            }
            else
            {
                Death();
            }
        }

        private void CollidedWithEnemy()
        {
            Logger.Debug("Collided with enemy");
            GetHit();
        }

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();

            if (levelManager == null)
            {
                Logger.Warn("LevelManager instance is not set on PlayerLife object on {} GameObject", name);
            }
        }

        private void Start()
        {
            Lives = startingLives;
        }
    }
}
