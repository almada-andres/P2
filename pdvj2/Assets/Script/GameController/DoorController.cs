using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject puertaAbiertaIzquierda;
    [SerializeField] private GameObject puertaAbiertaDerecha;
    [SerializeField] private ParticleSystem agujeroNegroParticulas;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        puertaAbiertaIzquierda.SetActive(false);
        puertaAbiertaDerecha.SetActive(false);
    }

    public void AbrirPuerta()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        puertaAbiertaIzquierda.SetActive(true);
        puertaAbiertaDerecha.SetActive(true);

        GenerarAgujeroNegro();

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void GenerarAgujeroNegro()
    {
        if (agujeroNegroParticulas != null)
        {
            agujeroNegroParticulas.Play();
        }
    }
}