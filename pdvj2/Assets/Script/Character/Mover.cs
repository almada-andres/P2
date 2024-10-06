using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

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

        // Crear el vector de dirección una sola vez
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        // Normalizar solo si es necesario
        if (direccion.magnitude > Mathf.Epsilon)
        {
            direccion.Normalize();
        }

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizar los parámetros en el Animator
        animator.SetBool("moviendo", direccion.magnitude > Mathf.Epsilon);
        animator.SetFloat("Velocidad", direccion.magnitude);

        // Invertir sprite según el movimiento horizontal actual
        spriteRenderer.flipX = movimientoHorizontal < 0;
    }
}