using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMPro.TextMeshProUGUI timerText;
    public int maxHealth = 10;
    public int currentHealth;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private int jumpCount = 2;
    private bool canChangeColor = true;

    private float elapsedTime = 0f;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        elapsedTime += Time.deltaTime;
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = "Time: " + minutes + " . " + seconds;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canChangeColor = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canChangeColor = true;
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

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    public void ChangeToRed()
    {
        if (canChangeColor)
        {
            spriteRenderer.color = Color.red;
        }
    }

    public void ChangeToGreen()
    {
        if (canChangeColor)
        {
            spriteRenderer.color = Color.green;
        }
    }

    public void ChangeToBlue()
    {
        if (canChangeColor)
        {
            spriteRenderer.color = Color.blue;
        }
    }
}