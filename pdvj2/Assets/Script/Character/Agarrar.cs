using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public AudioClip sonidoAbrirCofre;
    private AudioSource audioSource;

    void Start()
    {
        // Para cargar el audio
        audioSource = GetComponent<AudioSource>();

        // Si el jugador no tiene un AudioSource, se a�ade autom�ticamente
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

            // Destruir el cofre despu�s de un peque�o retraso para asegurarse de que el sonido se reproduzca
            Destroy(collision.gameObject, sonidoAbrirCofre.length);
        }
    }
}
