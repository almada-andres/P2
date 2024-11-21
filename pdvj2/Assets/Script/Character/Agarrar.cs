using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public AudioClip sonidoAbrirCofre;      // Sonido de abrir el cofre
    public AudioClip sonidoRecogerLlave;   // Sonido de recoger la llave
    public ParticleSystem particulasCofre;  // Partículas al abrir el cofre
    private AudioSource audioSource;        // Componente de AudioSource
    public GameController gameController;   // Controlador del juego
    public SistemaProgreso sistemaProgreso; // Sistema de progreso del juego
    private Inventory inventory;            // Inventario del jugador

    private bool visible = true;            // Estado de visibilidad del jugador

    public Image imageLlave;               // Referencia al Image de la llave en el HUD
    private bool tieneLlave = false;       // Estado de si el jugador tiene la llave

    public void SetVisibilidad(bool estado)
    {
        visible = estado;
    }

    public bool EsVisible()
    {
        return visible;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        inventory = FindObjectOfType<Inventory>();

        // Asegurarse de que la imagen de la llave esté desactivada al inicio
        if (imageLlave != null)
        {
            imageLlave.enabled = false; // Desactivar la imagen al inicio
        }
        else
        {
            Debug.LogWarning("No se ha asignado la imagen de la llave en el HUD.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cofre"))
        {
            if (tieneLlave) // Verifica si el jugador tiene la llave
            {
                // Reproducir el sonido de abrir el cofre
                audioSource.PlayOneShot(sonidoAbrirCofre);

                // Activar las partículas del cofre si están asignadas
                if (particulasCofre != null)
                {
                    particulasCofre.transform.position = collision.transform.position;
                    particulasCofre.Play();
                }

                // Actualizar el progreso del nivel
                sistemaProgreso.AbrirCofre();

                if (sistemaProgreso.progresoNivel.cofresAbiertos >= sistemaProgreso.progresoNivel.totalCofres)
                {
                    Debug.Log("¡Victoria! Has recogido todos los cofres.");
                    gameController.Victory();
                }

                // Destruir el cofre después de reproducir sonido y partículas
                float tiempoDestruccion = Mathf.Max(sonidoAbrirCofre.length, particulasCofre.main.duration);
                Destroy(collision.gameObject, tiempoDestruccion);

                // Eliminar la llave si solo se usa una vez
                tieneLlave = false;
                ActualizarIconoLlave();
            }
            else
            {
                Debug.Log("Necesitas una llave para abrir el cofre");
            }
        }

        if (collision.CompareTag("Llave"))
        {
            Debug.Log("El jugador ha colisionado con una llave.");

            // Reproducir el sonido de recoger la llave
            if (sonidoRecogerLlave != null)
            {
                audioSource.PlayOneShot(sonidoRecogerLlave);
            }

            // Añadir la llave al inventario
            inventory.AddItem("llave");
            Debug.Log("Llave añadida al inventario.");

            // Actualizar el progreso del nivel
            sistemaProgreso.ObtenerLlave();

            // Activar el icono de la llave en el HUD
            tieneLlave = true;
            ActualizarIconoLlave();

            // Destruir el objeto de la llave
            Destroy(collision.gameObject);
            Debug.Log("Objeto de la llave destruido.");
        }
    }

    private void ActualizarIconoLlave()
    {
        // Actualizar la visibilidad del icono de la llave en el HUD
        if (imageLlave != null)
        {
            imageLlave.enabled = tieneLlave; // Se activa si el jugador tiene la llave, de lo contrario se desactiva
        }
    }
}