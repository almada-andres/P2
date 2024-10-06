using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variables para almacenar la última dirección
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

        // Crear el vector de dirección
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        // Normalizar si es necesario
        if (direccion.magnitude > Mathf.Epsilon)
        {
            direccion.Normalize();
        }

        // Aplicar la velocidad al Rigidbody
        rb.velocity = direccion * velocidadMovimiento;

        // Actualizar el Animator
        animator.SetFloat("Velocidad", direccion.magnitude);

        // Actualizar la orientación del sprite
        spriteRenderer.flipX = lastHorizontalMovement < 0;

        // Almacenar la última dirección
        lastHorizontalMovement = direccion.x;
        lastVerticalMovement = direccion.y;
    }
}