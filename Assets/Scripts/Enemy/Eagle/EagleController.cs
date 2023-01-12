using UnityEngine;

namespace MIIProjekt.Enemy.Eagle
{
    [RequireComponent(typeof(Animator))]
    public class EagleController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;
        private Vector2 spawnPoint;
        private float currentAttackCooldown = 0.0f;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private float speed;

        [SerializeField]
        private float attackCooldown;

        [SerializeField]
        private float attackRange;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spawnPoint = transform.position;
        }

        private void Update()
        {
            if (!animator.GetBool("isDead"))
            {
                if(CanChaseAPlayer())
                {
                    ChaseAPlayer();
                }
                else
                {
                    ReturnToSpawn();
                }
                currentAttackCooldown = Mathf.Max(0, currentAttackCooldown - Time.deltaTime);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        private bool CanChaseAPlayer()
        {
            return currentAttackCooldown == 0.0f;
        }

        private void ChaseAPlayer()
        {
            Vector2 distance = target.position - transform.position;
            float distanceSqr = distance.sqrMagnitude;
            if(distance.magnitude <= attackRange)
            {
                GoInDirection(FindDirectionToTarget());
            }
            else
            {
                ReturnToSpawn();
            }
        }

        private void ReturnToSpawn()
        {
            Vector2 currentPosition = transform.position;
            float distanceSqr = (spawnPoint - currentPosition).sqrMagnitude;
            if (distanceSqr > 0.9f)
            {
                Vector2 directionToSpawn = FindDirectionToSpawn();
                GoInDirection(directionToSpawn);
            }
        }

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

        private Vector2 FindDirectionToSpawn()
        {
            Vector2 currentPosition = transform.position;
            Vector2 difference = spawnPoint - currentPosition;
            difference += Vector2.up * 0.5f;
            return difference.normalized;
        }

        private void GoInDirection(Vector2 direction)
        {
            Vector2 velocity = direction * speed;
            spriteRenderer.flipX = velocity.x > 0.0f;
            transform.position += (Vector3)(velocity * Time.deltaTime);
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
            else if (currentAttackCooldown == 0.0f)
            {
                collider.SendMessage("CollidedWithEnemy");
                currentAttackCooldown = attackCooldown;
            }
        }
    }
}
