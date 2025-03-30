using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    public int maxHealth = 10;
    public int currentHealth;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private int jumpCount = 2;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over!");
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
}