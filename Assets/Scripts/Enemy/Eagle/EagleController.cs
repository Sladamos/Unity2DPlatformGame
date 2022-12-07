using UnityEngine;

namespace MIIProjekt.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EagleController : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField]
        private float speed;

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;

        private Vector2 GetTargetDirection()
        {
            if (target == null)
            {
                return Vector2.zero;
            }

            Vector2 difference = target.position - transform.position;
            difference += Vector2.up * 0.5f;
            return difference.normalized;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!animator.GetBool("isDead"))
            {
                Vector2 velocity = GetTargetDirection() * speed;
                spriteRenderer.flipX = velocity.x > 0.0f;
                transform.position += (Vector3)(velocity * Time.deltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;
            Debug.Log($"bool: {animator.GetBool("isDead")}");
            
            if (animator.GetBool("isDead") || !collider.CompareTag("Player"))
            {
                return;
            }

            Debug.Log($"{collider.transform.position.y} > {transform.position.y}");

            if (collider.transform.position.y > transform.position.y)
            {
                animator.SetBool("isDead", true);
                GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                collider.SendMessage("CollidedWithEnemy");
            }
        }
        private void SetInactive()
        {
            gameObject.SetActive(false);
        }
    }
}
