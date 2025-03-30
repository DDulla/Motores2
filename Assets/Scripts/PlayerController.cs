using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    private int jumpCount = 2;
    public SpriteRenderer spriteRenderer;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount--;
            }
        }
    }

    public void ChangeToRed()
    {
        spriteRenderer.color = Color.red;
    }

    public void ChangeToGreen()
    {
        spriteRenderer.color = Color.green;
    }

    public void ChangeToBlue()
    {
        spriteRenderer.color = Color.blue;
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }
}