using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niebla : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Jugador jugador = other.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.SetVisibilidad(false); // El jugador no es visible
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Jugador jugador = other.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.SetVisibilidad(true); // El jugador vuelve a ser visible
            }
        }
    }
}

