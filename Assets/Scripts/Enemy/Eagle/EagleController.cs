using UnityEngine;

namespace MIIProjekt.Enemy.Eagle
{
    [RequireComponent(typeof(Animator))]
    public class EagleController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private float speed;

        private Vector2 FindDirectionToTarget()
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
                Vector2 velocity = FindDirectionToTarget() * speed;
                spriteRenderer.flipX = velocity.x > 0.0f;
                transform.position += (Vector3)(velocity * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (animator.GetBool("isDead") || !collider.CompareTag("Player"))
            {
                return;
            }

            if (collider.transform.position.y > transform.position.y)
            {
                animator.SetBool("isDead", true);
            }
            else
            {
                collider.SendMessage("CollidedWithEnemy");
            }
        }
    }
}
