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

        Vector2
 direccion = new Vector2(movimientoHorizontal, movimientoVertical);

        rb.velocity = direccion * velocidadMovimiento;

        // Actualizar animaciones (ajusta los nombres de los parámetros según tus animaciones)
        animator.SetFloat("Velocidad", Mathf.Abs(movimientoHorizontal));

        // Invertir sprite si se mueve hacia la izquierda
        spriteRenderer.flipX = movimientoHorizontal < 0;
    }
}   