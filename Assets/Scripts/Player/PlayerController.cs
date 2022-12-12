using UnityEngine;

namespace MIIProjekt.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float rayLength = 0.25f;

        [Range(0.01f, 20.0f)]
        [SerializeField]
        private float moveSpeed = 0.1f;

        [SerializeField]
        public float jumpForce = 6.0f;

        public LayerMask groundLayer;

        private Rigidbody2D myRigidbody;
        private Animator animator;
        private bool isWalking = false;
        private bool isFacingRight = true;
        private Vector2 startPosition;

        public void Finish()
        {
            myRigidbody.bodyType = RigidbodyType2D.Static;
            animator.SetBool("isWalking", false);
            animator.SetBool("isGrounded", true);
            enabled = false;
        }

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            isWalking = false;
            Vector2 input = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                input.x += 1;
                isWalking = true;
                if (!isFacingRight)
                {
                    Flip();
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                input.x += -1;
                isWalking = true;
                if (isFacingRight)
                {
                    Flip();
                }
            }

            Vector2 velocity = input * moveSpeed * Time.deltaTime;

            transform.Translate(velocity.x, 0.0f, 0.0f, Space.World);

            //Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.blue, 0.2f, false);
            animator.SetBool("isGrounded", IsGrounded());
            animator.SetBool("isWalking", isWalking);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    Jump();
                }
            }
        }

        private void Jump()
        {
            Debug.Log("Jumping");
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer.value);
        }

        private void ReturnToSpawn()
        {
            transform.position = startPosition;
        }
    }
}
