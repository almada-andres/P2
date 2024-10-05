using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    // Almacenamos la última dirección horizontal
    private float ultimaDireccionHorizontal;

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


        // Creamos el vector de dirección considerando ambos ejes
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        // Normalizamos el vector para mantener una velocidad constante en diagonal
        direccion.Normalize();

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizamos la última dirección horizontal solo si hay movimiento horizontal
        if (movimientoHorizontal != 0)
        {
            ultimaDireccionHorizontal = movimientoHorizontal;
        }

        // Invertimos el sprite según la última dirección horizontal
        spriteRenderer.flipX = ultimaDireccionHorizontal < 0;

        Vector2
 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizar animaciones (ajusta los nombres de los parámetros según tus animaciones)
        animator.SetFloat("Velocidad", Mathf.Abs(movimientoHorizontal));

        // Invertir sprite si se mueve hacia la izquierda
        spriteRenderer.flipX = movimientoHorizontal < 0;

    }
}   