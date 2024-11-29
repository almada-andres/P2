using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool controlesInvertidos = false;

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

        // Verificamos si los controles están invertidos y los invertimos si es necesario
        if (controlesInvertidos)
        {
            movimientoHorizontal *= -1;
            movimientoVertical *= -1;
        }

        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        if (direccion.magnitude > Mathf.Epsilon)
        {
            direccion.Normalize();
        }

        rb.MovePosition(rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);

        // Actualizar el Animator
        animator.SetFloat("Velocidad", direccion.magnitude);

        // Actualizar la orientacion del sprite
        spriteRenderer.flipX = direccion.x < 0;

        // Almacenar la ultima direccion
        lastHorizontalMovement = direccion.x;
        lastVerticalMovement = direccion.y;
    }

    // Método para habilitar o deshabilitar la inversión de controles
    public void EstablecerControlesInvertidos(bool estado)
    {
        controlesInvertidos = estado;
    }
}