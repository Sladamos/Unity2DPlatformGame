using System;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerLife : MonoBehaviour
    {
        public event Action<int> PlayerLifeChanged;

        [SerializeField]
        private int startingLives = 3;

        [SerializeField]
        private int maximumLives = 5;

        private Rigidbody2D rb;
        private PlayerController pc;
        private Animator anim;
        private int lives = 0;

        public int Lives
        {
            get => lives;
            private set
            {
                lives = value;
                PlayerLifeChanged?.Invoke(value);
                Debug.Log("Current number of lives: " + Lives);
            }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            pc = GetComponent<PlayerController>();

            Lives = startingLives;
        }

        public bool CanPickupBonusLife()
        {
            return lives < maximumLives;
        }

        private void GetHit()
        {
            DecreaseLives(1);
            if (Lives > 0)
                this.SendMessage("ReturnToSpawn");
            else
                Death();
        }

        private void Death()
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            pc.enabled = false;
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
        }

        private void CollidedWithEnemy()
        {
            GetHit();
            Debug.Log("Collided with enemy");
        }
    }
}
