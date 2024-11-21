using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float lastHorizontalMovement;
    private float lastVerticalMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        if (direccion.magnitude > Mathf.Epsilon)
        {
            direccion.Normalize();
        }

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizar el Animator
        animator.SetFloat("Velocidad", direccion.magnitude);

        // Actualizar la orientacion del sprite
        spriteRenderer.flipX = lastHorizontalMovement < 0;

        // Almacenar la ultima direccion
        lastHorizontalMovement = direccion.x;
        lastVerticalMovement = direccion.y;
    }
}