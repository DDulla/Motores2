using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private int jumpCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Unity Event para movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Unity Event para salto
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) // Detectar cuándo se presiona
        {
            if (isGrounded || jumpCount < 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;
            }
        }
    }

    private void Update()
    {
        // Movimiento horizontal
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // Validar si está en el suelo usando Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0; // Reiniciar el contador de doble salto
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.1f);
    }
}
