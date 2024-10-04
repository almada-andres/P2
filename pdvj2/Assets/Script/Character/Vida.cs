using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;
    public GameController gameController; // Referencia al GameController
    public AudioClip sonidoDa�o; // Sonido que se reproducir� al recibir da�o
    private AudioSource audioSource; // Referencia al componente AudioSource
    private Animator animator; // Referencia al Animator

    void Start()
    {
        vidaActual = vidaMaxima;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // Verificar si hay un AudioSource y a�adir uno si no existe
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RestarVida(int da�o)
    {
        vidaActual -= da�o;

        // Reproducir sonido de da�o
        if (sonidoDa�o != null)
        {
            audioSource.PlayOneShot(sonidoDa�o);
        }

        // Activar la animaci�n de "Da�o" si el jugador no est� muerto
        if (vidaActual > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Da�o");
            }
        }
        else if (vidaActual <= 0)
        {
            // Si la vida llega a 0, activar el estado "Muerto"
            if (animator != null)
            {
                animator.SetBool("Muerto", true); // Establecer el par�metro Muerto a true
            }

            Debug.Log("Jugador derrotado");
            gameController.Defeat(); // Llamar al m�todo de derrota
        }
    }
}