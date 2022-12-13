using System;
using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerLife : MonoBehaviour
    {
        public event Action<int> PlayerLifeChanged;

        [SerializeField]
        private int startingLives = 3;

        private Rigidbody2D rb;
        private PlayerController pc;
        private Animator anim;
        private int lives = 0;

        private int Lives
        {
            get => lives;
            set
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
            Lives += numberOfLives;
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
