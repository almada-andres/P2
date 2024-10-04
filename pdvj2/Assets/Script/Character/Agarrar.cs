using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public AudioClip sonidoAbrirCofre; // Sonido al abrir el cofre
    public ParticleSystem particulasCofre; // Sistema de part�culas para el cofre
    private AudioSource audioSource;
    public GameController gameController; // Referencia al GameController
    private int cofresRecogidos = 0; // Contador de cofres recogidos

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cofre"))
        {
            // Reproducir el sonido de abrir cofre
            audioSource.PlayOneShot(sonidoAbrirCofre);

            // Reproducir el sistema de part�culas
            if (particulasCofre != null)
            {
                particulasCofre.transform.position = collision.transform.position;
                particulasCofre.Play();
            }

            // Aumentar el contador de cofres recogidos
            cofresRecogidos++;

            // Comprobar si se han recogido todos los cofres
            if (cofresRecogidos >= 1) // Se puede ir cambiando a medida que agrego cofres
            {
                Debug.Log("�Victoria! Has recogido todos los cofres.");
                gameController.Victory(); // Llamar al m�todo de victoria en GameController
            }

            // Destruir el cofre despu�s de la duraci�n m�xima entre el sonido y las part�culas
            float tiempoDestruccion = Mathf.Max(sonidoAbrirCofre.length, particulasCofre.main.duration);
            Destroy(collision.gameObject, tiempoDestruccion);
        }
    }
}