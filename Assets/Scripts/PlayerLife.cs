using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private FoxController fc;
    private Animator anim;
    private int lives = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fc = GetComponent<FoxController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        DecreaseLives(1);
        if (lives > 0)
            this.SendMessage("ReturnToSpawn");
        else
            Death();
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        fc.enabled = false;
    }

    private void IncreaseLives(int numberOfLives)
    {
        lives += numberOfLives;
        Debug.Log("Current number of lives: " + lives);
    }

    private void DecreaseLives(int numberOfLives)
    {
        lives -= numberOfLives;
        Debug.Log("Current number of lives: " + lives);
    }
    private void CollidedWithEnemy()
    {
        Debug.Log("Collided with enemy");
    }
}
