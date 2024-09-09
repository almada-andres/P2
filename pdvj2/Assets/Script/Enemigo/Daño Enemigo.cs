using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    public int daño = 1; // Daño que inflige el enemigo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            // Obtener el script del jugador
            Vida jugador = collision.GetComponent<Vida>();
            if (jugador != null)
            {
                jugador.RestarVida(daño); // Llamar a la función para restar vida en el script del jugador
            }

            // Destruir al enemigo
            Destroy(gameObject);
        }
    }
}