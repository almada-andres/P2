using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public GameController gameController;
    public AudioClip sonidoDaño;
    private AudioSource audioSource;
    private Animator animator;
    public ProgresoNivelSO progresoNivel; // Referencia al Scriptable Object

    void Start()
    {
        progresoNivel.ResetearProgreso(); // Resetear el progreso al inicio si es necesario
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
        progresoNivel.vidaActual -= daño;

        if (progresoNivel.vidaActual <= 0)
        {
            progresoNivel.vidaActual = 0; // Asegurarse de que la vida no sea negativa
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

        Debug.Log("Vida restante: " + progresoNivel.vidaActual);
    }

    // Propiedades para encapsular vidaMaxima y vidaActual
    public int VidaMaxima { get { return progresoNivel.vidaMaxima; } set { progresoNivel.vidaMaxima = value; } }
    public int VidaActual { get { return progresoNivel.vidaActual; } }
}