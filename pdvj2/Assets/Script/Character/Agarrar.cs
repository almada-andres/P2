using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Jugador : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoAbrirCofre;
    [SerializeField] private AudioClip sonidoRecogerLlave;
    [SerializeField] private ParticleSystem particulasCofre;
    private AudioSource audioSource;

    [SerializeField] private SistemaProgreso sistemaProgreso;
    private Inventory inventory;

    private bool visible = true;
    private bool estaVivo = true;

    [SerializeField] private Image imageLlave;
    [SerializeField] private bool tieneLlave = false;

    [SerializeField] private Image imageLlaveDorada;
    [SerializeField] private bool tieneLlaveDorada = false;

    private bool efectoHongoActivo = false;
    private float duracionEfectoRestante = 0f;

    [SerializeField] private GameObject spriteCofreActivo;
    [SerializeField] private GameObject memoryHUD;

    [SerializeField] private TextMeshProUGUI Stage2No;

    private Mover mover;

    public void SetVisibilidad(bool estado)
    {
        visible = estado;
    }

    public bool EsVisible()
    {
        return visible;
    }

    public bool EfectoActivo => efectoHongoActivo;

    public void SetEstadoVivo(bool estado)
    {
        estaVivo = estado;
    }

    void Start()
    {
        if (Stage2No != null)
        {
            Stage2No.gameObject.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        inventory = FindObjectOfType<Inventory>();

        if (imageLlave != null)
        {
            imageLlave.enabled = false;
        }

        if (imageLlaveDorada != null)
        {
            imageLlaveDorada.enabled = false;
        }

        if (spriteCofreActivo != null)
        {
            spriteCofreActivo.SetActive(false);
        }

        if (memoryHUD != null)
        {
            memoryHUD.SetActive(false);
        }

        // Obtener la referencia al componente Mover
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (!estaVivo) return;

        if (efectoHongoActivo)
        {
            duracionEfectoRestante -= Time.deltaTime;
            if (duracionEfectoRestante <= 0)
            {
                TerminarEfectoHongo();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cofre"))
        {
            if (tieneLlave)
            {
                audioSource.PlayOneShot(sonidoAbrirCofre);

                if (particulasCofre != null)
                {
                    particulasCofre.transform.position = collision.transform.position;
                    particulasCofre.Play();
                }

                if (memoryHUD != null)
                {
                    memoryHUD.SetActive(true);
                }

                if (spriteCofreActivo != null)
                {
                    spriteCofreActivo.SetActive(true);
                }

                sistemaProgreso.AbrirCofre();
                ActivarLlaveDorada();

                float tiempoDestruccion = Mathf.Max(sonidoAbrirCofre.length, particulasCofre.main.duration);
                Destroy(collision.gameObject, tiempoDestruccion);

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
            if (sonidoRecogerLlave != null)
            {
                audioSource.PlayOneShot(sonidoRecogerLlave);
            }

            inventory.AddItem("llave");
            sistemaProgreso.ObtenerLlave();

            tieneLlave = true;
            ActualizarIconoLlave();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Hongo"))
        {
            if (!efectoHongoActivo)
            {
                float duracionEfecto = 5f;
                ActivarEfectoHongo(duracionEfecto);
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Stage2"))
        {
            GameManager.Instance.Victory("Stage2");
        }

        if (collision.CompareTag("Stage2No"))
        {
            if (tieneLlaveDorada)
            {
                Debug.Log("Accediendo al nivel 2...");
            }
            else
            {
                MostrarMensaje("Necesitas la Llave Dorada para acceder.");
            }
        }
    }

    private void ActualizarIconoLlave()
    {
        if (imageLlave != null)
        {
            imageLlave.enabled = tieneLlave;
        }
    }

    private void ActivarLlaveDorada()
    {
        tieneLlaveDorada = true;

        if (imageLlaveDorada != null)
        {
            imageLlaveDorada.enabled = true;
        }
    }

    public void ActivarEfectoHongo(float duracion)
    {
        efectoHongoActivo = true;
        // Metodo para invertir los controles
        mover.EstablecerControlesInvertidos(true);
        duracionEfectoRestante = duracion;
    }

    private void TerminarEfectoHongo()
    {
        mover.EstablecerControlesInvertidos(false); // Restablecemos los controles a la normalidad
        efectoHongoActivo = false;
    }

    private void MostrarMensaje(string mensaje)
    {
        if (Stage2No != null)
        {
            Stage2No.text = mensaje;
            Stage2No.gameObject.SetActive(true);

            Invoke(nameof(OcultarMensaje), 3f);
        }
    }

    private void OcultarMensaje()
    {
        if (Stage2No != null)
        {
            Stage2No.gameObject.SetActive(false);
        }
    }
}