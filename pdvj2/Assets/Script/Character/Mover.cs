using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    // Almacenamos la �ltima direcci�n horizontal
    private float ultimaDireccionHorizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Creamos el vector de direcci�n considerando ambos ejes
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        // Normalizamos el vector para mantener una velocidad constante en diagonal
        direccion.Normalize();

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizamos la �ltima direcci�n horizontal solo si hay movimiento horizontal
        if (movimientoHorizontal != 0)
        {
            ultimaDireccionHorizontal = movimientoHorizontal;
        }

        // Invertimos el sprite seg�n la �ltima direcci�n horizontal
        spriteRenderer.flipX = ultimaDireccionHorizontal < 0;
    }
}