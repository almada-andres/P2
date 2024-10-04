using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public AudioClip sonidoAbrirCofre; // Sonido al abrir el cofre
    public ParticleSystem particulasCofre; // Sistema de partículas para el cofre
    private AudioSource audioSource;

    // Variable para controlar si el cofre ha sido recogido
    private bool cofreRecogido = false;

    void Start()
    {
        // Obtener el componente AudioSource del jugador
        audioSource = GetComponent<AudioSource>();

        // Si el jugador no tiene un AudioSource, se añade automáticamente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cofre") && !cofreRecogido) // Verifica que el cofre no haya sido recogido
        {
            cofreRecogido = true; // Marcar el cofre como recogido

            // Reproducir el sonido de abrir cofre
            audioSource.PlayOneShot(sonidoAbrirCofre);

            // Reproducir el sistema de partículas
            if (particulasCofre != null)
            {
                // Colocar las partículas en la posición del cofre
                particulasCofre.transform.position = collision.transform.position;

                // Activar las partículas
                particulasCofre.Play();
            }

            // Destruir el cofre después de la duración máxima entre el sonido y las partículas
            float tiempoDestruccion = Mathf.Max(sonidoAbrirCofre.length, particulasCofre.main.duration);
            Destroy(collision.gameObject, tiempoDestruccion);
        }
    }
}