using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private FoxController fc;
    private Animator anim;

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
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        fc.enabled = false;
    }

    private void CollidedWithEnemy()
    {
        Debug.Log("Collided with enemy");
    }
}
