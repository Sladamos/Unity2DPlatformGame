using System;
using MIIProjekt.GameManagers;
using NLog;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerLife : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public event Action<int, int> PlayerLifeChanged;
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

                lives = Mathf.Max(0, value);

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
            SendMessage("PlayerDied");
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

        private void CollidedWithSpike()
        {
            Logger.Debug("Collided with spike");
            GetHit();
        }


        private void Awake()
        {

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
