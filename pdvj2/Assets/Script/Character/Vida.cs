using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    public AudioClip sonidoDa�o;
    private AudioSource audioSource;
    private Animator animator; 

    void Start()
    {
        vidaActual = vidaMaxima;

        // Obtener la referencia del Animator
        animator = GetComponent<Animator>();

        // Obtener la referencia del AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si el objeto no tiene un AudioSource se a�ade autom�ticamente
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RestarVida(int da�o)
    {
        vidaActual -= da�o;

        Debug.Log("Vida restante: " + vidaActual);

        // Reproducir el sonido de da�o
        if (audioSource != null && sonidoDa�o != null)
        {
            audioSource.PlayOneShot(sonidoDa�o);
        }

        // Activar la animaci�n de "Da�o" cuando el jugador reciba da�o
        if (animator != null)
        {
            animator.SetTrigger("Da�o");
        }

        // Comprobar si la vida llega a 0
        if (vidaActual <= 0)
        {
            Debug.Log("Jugador derrotado");
        }
    }
}