using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Vida : MonoBehaviour
{
    public GameController gameController;
    public AudioClip sonidoDaño;
    private AudioSource audioSource;
    private Animator animator;
    public ProgresoNivelSO progresoNivel; // Referencia al Scriptable Object

    public List<Image> iconosVida; // Lista de íconos de vida en el Canvas
    public UnityEvent onVidaModificada; // Evento para acciones adicionales al restar vida

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

        ActualizarIconosVida(); // Inicializa la visualización de los corazones
    }

    public void RestarVida(int daño)
    {
        progresoNivel.vidaActual -= daño;

        if (progresoNivel.vidaActual <= 0)
        {
            progresoNivel.vidaActual = 0; // Asegura que la vida no sea negativa
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

        // Llama al evento de actualización de vida
        onVidaModificada.Invoke();

        // Actualiza los íconos de vida visualmente
        ActualizarIconosVida();
    }

    private void ActualizarIconosVida()
    {
        // Itera sobre los íconos de vida y actualiza su visibilidad
        for (int i = 0; i < iconosVida.Count; i++)
        {
            if (i < progresoNivel.vidaActual)
            {
                iconosVida[i].enabled = true; // Muestra el ícono si hay vida
            }
            else
            {
                iconosVida[i].enabled = false; // Oculta el ícono si la vida fue restada
            }
        }
    }

    public void AgregarVida(int cantidad)
    {
        if (progresoNivel.vidaActual >= progresoNivel.vidaMaxima)
        {
            Debug.Log("El jugador ya tiene la vida máxima.");
            return; // Si la vida está en el máximo, no hace nada
        }

        // Calcula la cantidad de vida que se puede agregar sin superar el máximo
        int vidaRestanteParaMaximo = progresoNivel.vidaMaxima - progresoNivel.vidaActual;
        int vidaAAgregar = Mathf.Min(cantidad, vidaRestanteParaMaximo);

        progresoNivel.vidaActual += vidaAAgregar;

        // Actualiza los íconos de vida en pantalla
        ActualizarIconosVida();
        Debug.Log("Vida añadida: " + vidaAAgregar + " | Vida actual: " + progresoNivel.vidaActual);
    }

    // Propiedades para encapsular vidaMaxima y vidaActual
    public int VidaMaxima { get { return progresoNivel.vidaMaxima; } set { progresoNivel.vidaMaxima = value; } }
    public int VidaActual { get { return progresoNivel.vidaActual; } }
}