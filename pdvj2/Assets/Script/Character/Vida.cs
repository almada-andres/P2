using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Vida : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip sonidoDaño;
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] private ProgresoNivelSO progresoNivel;

    [Header("Vida y Luces 2D")]
    [SerializeField] private List<Image> iconosVida; 
    [SerializeField] private List<Light2D> lucesVida;

    [SerializeField] private UnityEvent onVidaModificada;

    void Start()
    {
        progresoNivel.ResetearProgreso();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        ActualizarIconosVida();
    }

    public void RestarVida(int daño)
    {
        progresoNivel.VidaActual -= daño;

        if (progresoNivel.VidaActual <= 0)
        {
            progresoNivel.VidaActual = 0; // Asegura que la vida no sea negativa

            if (animator != null)
            {
                // Resetea cualquier trigger o parámetro que esté activo
                foreach (AnimatorControllerParameter parametro in animator.parameters)
                {
                    if (parametro.type == AnimatorControllerParameterType.Bool)
                    {
                        animator.SetBool(parametro.name, false);
                    }
                    else if (parametro.type == AnimatorControllerParameterType.Trigger)
                    {
                        animator.ResetTrigger(parametro.name);
                    }
                }
                animator.Play("Muerto");
            }

            DesactivarMovimiento();

            Jugador jugador = GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.SetVisibilidad(false);
            }
            gameManager.Defeat();
        }
        else
        {
            // Reproducir sonido de daño y activar animación
            audioSource.PlayOneShot(sonidoDaño);

            if (animator != null)
            {
                animator.SetTrigger("Daño");
            }

            // Activar el resplandor de daño
            Resplandor resplandor = GetComponent<Resplandor>();
            EfectoSangre efectoSangre = GetComponent<EfectoSangre>();

            if (resplandor != null && efectoSangre != null)
            {
                efectoSangre.EmitirSangre();
                resplandor.Luz();
            }
        }

        Debug.Log("Vida restante: " + progresoNivel.VidaActual);

        onVidaModificada.Invoke();

        ActualizarIconosVida();
    }

    private void DesactivarMovimiento()
    {
        Jugador jugador = GetComponent<Jugador>();
        if (jugador != null)
        {
            jugador.SetEstadoVivo(false); // Marca al jugador como muerto
        }
        // Desactiva el script de movimiento
        Mover movimiento = GetComponent<Mover>();
        if (movimiento != null)
        {
            movimiento.enabled = false;
        }

        // Detener el Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Detener al personaje
            rb.isKinematic = true;
        }

        // Desactivar otros componentes
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false; // Desactiva el colisionador
        }
    }

    private void ActualizarIconosVida()
    {
        for (int i = 0; i < iconosVida.Count; i++)
        {
            if (i < progresoNivel.VidaActual)
            {
                iconosVida[i].enabled = true; // Muestra el icono si hay vida
                if (lucesVida.Count > i && lucesVida[i] != null)
                {
                    lucesVida[i].enabled = true; // Activa la luz 2D correspondiente
                }
            }
            else
            {
                iconosVida[i].enabled = false; // Oculta el icono si la vida fue restada
                if (lucesVida.Count > i && lucesVida[i] != null)
                {
                    lucesVida[i].enabled = false; // Apaga la luz 2D correspondiente
                }
            }
        }
    }

    public void AgregarVida(int cantidad)
    {
        if (progresoNivel.VidaActual >= progresoNivel.VidaMaxima)
        {
            Debug.Log("El jugador ya tiene la vida máxima.");
            return; // Si la vida está en el maximo, no hace nada
        }

        // Calcula la cantidad de vida que se puede agregar sin superar el maximo
        int vidaRestanteParaMaximo = progresoNivel.VidaMaxima - progresoNivel.VidaActual;
        int vidaAAgregar = Mathf.Min(cantidad, vidaRestanteParaMaximo);

        progresoNivel.VidaActual += vidaAAgregar;

        // Actualiza los íconos de vida en pantalla
        ActualizarIconosVida();
        Debug.Log("Vida añadida: " + vidaAAgregar + " | Vida actual: " + progresoNivel.VidaActual);
    }

    // Propiedades para encapsular vidaMaxima y vidaActual
    public int VidaMaxima
    {
        get
        {
            return progresoNivel.VidaMaxima;
        }
        set
        {
            progresoNivel.VidaMaxima = value;
        }
    }

    public int VidaActual
    {
        get
        {
            return progresoNivel.VidaActual;
        }
    }
}