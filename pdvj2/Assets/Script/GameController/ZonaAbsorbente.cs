using System.Collections.Generic;
using UnityEngine;

public class ZonaAbsorbente : MonoBehaviour
{
    [SerializeField] private Transform centroZona;
    [SerializeField] private float radioExternoAdicional = 5f;
    [SerializeField] private float radioExterno = 4f;
    [SerializeField] private float radioMedio = 3f;
    [SerializeField] private float radioInterno = 2f;

    [SerializeField] private int dañoPorRadio = 1;
    [SerializeField] private float fuerzaBaseAtraccion = 5f;

    private HashSet<GameObject> jugadoresAfectados = new HashSet<GameObject>();
    private Dictionary<GameObject, int> radiosVisitados = new Dictionary<GameObject, int>();

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            GameObject jugador = other.gameObject;

            // Aplicar fuerza de atracción
            float distancia = Vector2.Distance(jugador.transform.position, centroZona.position);
            AplicarFuerzaAtraccion(jugador, distancia);

            // Calcular el radio actual y aplicar daño si es necesario
            int radioActual = CalcularRadio(distancia);
            if (radioActual > 0) // Solo los radios que causan daño
            {
                if (!radiosVisitados.ContainsKey(jugador) || radiosVisitados[jugador] != radioActual)
                {
                    radiosVisitados[jugador] = radioActual;

                    // Aplicar daño al jugador usando el script Vida
                    Vida vidaJugador = jugador.GetComponent<Vida>();
                    if (vidaJugador != null)
                    {
                        vidaJugador.RestarVida(dañoPorRadio); // Se encarga del HUD y animaciones
                    }
                }
            }
        }
    }

    private void AplicarFuerzaAtraccion(GameObject jugador, float distancia)
    {
        Vector2 direccion = (centroZona.position - jugador.transform.position).normalized;
        float magnitudFuerza = fuerzaBaseAtraccion / distancia;

        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        if (rbJugador != null)
        {
            rbJugador.AddForce(direccion * magnitudFuerza, ForceMode2D.Force);
        }
    }

    private int CalcularRadio(float distancia)
    {
        if (distancia <= radioInterno) return 3;
        else if (distancia <= radioMedio) return 2;
        else if (distancia <= radioExterno) return 1;
        else if (distancia <= radioExternoAdicional) return 0;
        return -1; // Fuera de los radios
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            jugadoresAfectados.Remove(other.gameObject);
            radiosVisitados.Remove(other.gameObject); // Permite reiniciar el daño si vuelve a entrar
        }
    }
}