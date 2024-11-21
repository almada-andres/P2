using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 2f;
    public float rangoVision = 1f;
    private Rigidbody2D rb;
    private Jugador jugador;

    // Límites para patrullaje
    [SerializeField] private float limiteIzquierdo;
    [SerializeField] private float limiteDerecho;
    [SerializeField] private float limiteSuperior;
    [SerializeField] private float limiteInferior;

    // Dirección de movimiento
    private Vector2 direccion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindWithTag("Jugador").GetComponent<Jugador>();

        // Determinar la dirección inicial en función de los límites
        if (limiteIzquierdo != limiteDerecho)
        {
            direccion = Vector2.right; // Patrullaje horizontal
        }
        else if (limiteSuperior != limiteInferior)
        {
            direccion = Vector2.up; // Patrullaje vertical
        }
        else
        {
            Debug.LogWarning("Los límites del enemigo no están configurados correctamente.");
            direccion = Vector2.zero; // Sin movimiento
        }
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.transform.position);
            if (distanciaAlJugador <= rangoVision && jugador.EsVisible())
            {
                // Perseguir al jugador
                PerseguirJugador();
            }
            else
            {
                // Patrullar
                Patrullar();
            }
        }
    }

    private void PerseguirJugador()
    {
        Vector2 direccionJugador = (jugador.transform.position - transform.position).normalized;
        rb.velocity = direccionJugador * velocidad;
    }

    private void Patrullar()
    {
        if (direccion == Vector2.right || direccion == Vector2.left)
        {
            // Patrullaje horizontal
            if (transform.position.x >= limiteDerecho)
                direccion = Vector2.left;
            else if (transform.position.x <= limiteIzquierdo)
                direccion = Vector2.right;

            rb.velocity = new Vector2(direccion.x * velocidad, 0);
        }
        else if (direccion == Vector2.up || direccion == Vector2.down)
        {
            // Patrullaje vertical
            if (transform.position.y >= limiteSuperior)
                direccion = Vector2.down;
            else if (transform.position.y <= limiteInferior)
                direccion = Vector2.up;

            rb.velocity = new Vector2(0, direccion.y * velocidad);
        }
    }
}