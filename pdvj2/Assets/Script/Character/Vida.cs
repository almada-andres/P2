using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    public AudioClip sonidoDaño;
    private AudioSource audioSource;
    private Animator animator; 

    void Start()
    {
        vidaActual = vidaMaxima;

        // Obtener la referencia del Animator
        animator = GetComponent<Animator>();

        // Obtener la referencia del AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si el objeto no tiene un AudioSource se añade automáticamente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RestarVida(int daño)
    {
        vidaActual -= daño;

        Debug.Log("Vida restante: " + vidaActual);

        // Reproducir el sonido de daño
        if (audioSource != null && sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        // Activar la animación de "Daño" cuando el jugador reciba daño
        if (animator != null)
        {
            animator.SetTrigger("Daño");
        }

        // Comprobar si la vida llega a 0
        if (vidaActual <= 0)
        {
            Debug.Log("Jugador derrotado");
        }
    }
}