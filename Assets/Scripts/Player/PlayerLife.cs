using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController pc;
    private Animator anim;
    private int lives = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    private void GetHit()
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
        pc.enabled = false;
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
        GetHit();
        Debug.Log("Collided with enemy");
    }
}
