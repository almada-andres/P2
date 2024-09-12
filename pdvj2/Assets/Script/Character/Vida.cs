using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    // Sonido que se reproducirá al recibir daño
    public AudioClip sonidoDaño;

    private AudioSource audioSource;

    void Start()
    {
        // Inicializar la vida actual
        vidaActual = vidaMaxima;

        // Cargar el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si el objeto no tiene un AudioSource, lo añadimos automáticamente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RestarVida(int daño)
    {
        vidaActual -= daño;

        // Reproducir sonido de daño
        if (sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        Debug.Log("Vida restante: " + vidaActual);
    }
}
