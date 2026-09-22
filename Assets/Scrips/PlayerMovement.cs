using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float velocidad = 0.5f;
    private float fuerzaSalto = 2f;

    private Rigidbody2D rb;
    private bool estaEnSuelo = false;

    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movimiento = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movimiento = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movimiento = 1f;

        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && estaEnSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            estaEnSuelo = false;
        }

        animator.SetFloat("Movement", movimiento * velocidad);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
        }
    }
}