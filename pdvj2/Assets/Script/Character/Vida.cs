using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    private int vidaActual;

    public GameController gameController;
    public AudioClip sonidoDaño;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        vidaActual = vidaMaxima;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // Asegurarse de que el AudioSource exista
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RestarVida(int daño)
    {
        vidaActual -= daño;

        if (vidaActual <= 0)
        {
            vidaActual = 0; // Asegurarse de que la vida no sea negativa
            if (animator != null)
            {
                animator.SetBool("Muerto", true);
            }
            Debug.Log("Jugador derrotado");
            gameController.Defeat();
        }
        else
        {
            // Reproducir sonido de daño y activar animación
            audioSource.PlayOneShot(sonidoDaño);
            if (animator != null)
            {
                animator.SetTrigger("Daño");
            }
        }

        Debug.Log("Vida restante: " + vidaActual);
    }

    // Propiedades para encapsular vidaMaxima y vidaActual
    public int VidaMaxima { get { return vidaMaxima; } set { vidaMaxima = value; } }
    public int VidaActual { get { return vidaActual; } }
}