using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private ResultsManager resultsManager;
    [SerializeField] public List<MonoBehaviour> enemies;

    public UnityEvent<int> OnLifeChanged;
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<string> OnGameEnd;

    public int maxHealth = 10;
    public int currentHealth;
    private int score = 0;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private int jumpCount = 2;

    private Color[] colors = { Color.red, Color.green, Color.blue };
    private int currentColorIndex = 0;

    private float elapsedTime = 0f;
    private bool canChangeColor = true;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        spriteRenderer.color = colors[currentColorIndex];
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        elapsedTime += Time.deltaTime;
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = "Time: " + minutes + "." + seconds;
        }

        if (elapsedTime >= 70f)
        {
            resultsManager.SetGameData(elapsedTime, score);
            resultsManager.ShowResults("You win");
            OnGameEnd?.Invoke("You win");
            enabled = false;
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

    public void ChangeColorLeft(InputAction.CallbackContext context)
    {
        if (context.performed && canChangeColor)
        {
            currentColorIndex = (currentColorIndex - 1 + colors.Length) % colors.Length;
            UpdatePlayerColor();
        }
    }

    public void ChangeColorRight(InputAction.CallbackContext context)
    {
        if (context.performed && canChangeColor)
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            UpdatePlayerColor();
        }
    }

    private void UpdatePlayerColor()
    {
        spriteRenderer.color = colors[currentColorIndex];

        foreach (var enemy in enemies)
        {
            if (enemy is ObstacleRed)
            {
                ((ObstacleRed)enemy).UpdateMovementState(spriteRenderer.color);
            }
            else if (enemy is ObstacleGreen)
            {
                ((ObstacleGreen)enemy).UpdateMovementState(spriteRenderer.color);
            }
            else if (enemy is ObstacleBlue)
            {
                ((ObstacleBlue)enemy).UpdateMovementState(spriteRenderer.color);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            canChangeColor = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2; 
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnLifeChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            resultsManager.SetGameData(elapsedTime, score);
            resultsManager.ShowResults("Defeat");
            OnGameEnd?.Invoke("Defeat");
            enabled = false;
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnLifeChanged?.Invoke(currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}