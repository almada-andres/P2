using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoAbrirCofre;
    [SerializeField] private AudioClip sonidoRecogerLlave;
    [SerializeField] private ParticleSystem particulasCofre;
    private AudioSource audioSource;

    [SerializeField] private GameController gameController; 
    [SerializeField] private SistemaProgreso sistemaProgreso;
    private Inventory inventory; 

    private bool visible = true;            // Estado de visibilidad del jugador

    [SerializeField] private Image imageLlave;               // Referencia al Image de la llave en el HUD
    [SerializeField] private bool tieneLlave = false;       // Estado de si el jugador tiene la llave


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
                audioSource.PlayOneShot(sonidoAbrirCofre);

                if (particulasCofre != null)
                {
                    particulasCofre.transform.position = collision.transform.position;
                    particulasCofre.Play();
                }

                // Actualizar el progreso del nivel
                sistemaProgreso.AbrirCofre();

                // getter para acceder a totalCofres y cofresAbiertos
                if (sistemaProgreso.progresoNivel.GetCofresAbiertos() >= sistemaProgreso.progresoNivel.GetTotalCofres())
                {
                    Debug.Log("Has recogido todos los cofres.");
                    gameController.Victory();
                }

                // Destruir el cofre
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
            imageLlave.enabled = tieneLlave;
        }
    }
}