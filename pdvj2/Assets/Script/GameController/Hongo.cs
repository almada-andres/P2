using UnityEngine;

public class Hongo : MonoBehaviour
{
    [SerializeField] private float duracionEfecto = 5f; // Duración del efecto de inversión de controles
    [SerializeField] private AudioClip sonidoHongo;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Jugador"))
        {
            Jugador jugador = colision.GetComponent<Jugador>();
            if (jugador != null && !jugador.EfectoActivo)
            {
                jugador.ActivarEfectoHongo(duracionEfecto);
                if (sonidoHongo != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoHongo, transform.position);
                }
                Destroy(gameObject); // Elimina el hongo del juego
            }
        }
    }
}