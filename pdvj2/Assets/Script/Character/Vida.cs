using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;
    public GameController gameController; // Referencia al GameController
    public AudioClip sonidoDaño; // Sonido que se reproducirá al recibir daño
    private AudioSource audioSource; // Referencia al componente AudioSource
    private Animator animator; // Referencia al Animator

    void Start()
    {
        vidaActual = vidaMaxima;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // Verificar si hay un AudioSource y añadir uno si no existe
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

        // Activar la animación de "Daño" si el jugador no está muerto
        if (vidaActual > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Daño");
            }
        }
        else if (vidaActual <= 0)
        {
            // Si la vida llega a 0, activar el estado "Muerto"
            if (animator != null)
            {
                animator.SetBool("Muerto", true); // Establecer el parámetro Muerto a true
            }

            Debug.Log("Jugador derrotado");
            gameController.Defeat(); // Llamar al método de derrota
        }
    }
}