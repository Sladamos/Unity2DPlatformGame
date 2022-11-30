using UnityEngine;

public class FoxController : MonoBehaviour
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
    private int score = 0;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
                Flip();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            input.x += -1;
            isWalking = true;
            if (isFacingRight)
                Flip();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
                Jump();
        }

        Vector2 velocity = input * moveSpeed * Time.deltaTime;

        transform.Translate(velocity.x, 0.0f, 0.0f, Space.World);

        //Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.blue, 0.2f, false);
        animator.SetBool("isGrounded", IsGrounded());     
        animator.SetBool("isWalking", isWalking);     
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonus"))
        {
            score += 15;
            Debug.Log("Score: " + score);
            collision.gameObject.SetActive(false);
        }
    }
}
