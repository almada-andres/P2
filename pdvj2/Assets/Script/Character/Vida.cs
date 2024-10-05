using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    // Sonido que se reproducir� al recibir da�o
    public AudioClip sonidoDa�o;

    private AudioSource audioSource;

    void Start()
    {
        // Inicializar la vida actual
        vidaActual = vidaMaxima;

        // Cargar el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Si el objeto no tiene un AudioSource, lo a�adimos autom�ticamente
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

        Debug.Log("Vida restante: " + vidaActual);
    }
}
