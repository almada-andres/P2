using UnityEngine;

public class ObjetoVida : MonoBehaviour
{
    [SerializeField] private int cantidadVida = 1; // Cantidad de vida que otorga este objeto

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Vida vidaJugador = other.GetComponent<Vida>();

            if (vidaJugador != null)
            {
                // Solo a�ade vida si el jugador no est� en el maximo
                if (vidaJugador.VidaActual < vidaJugador.VidaMaxima)
                {
                    vidaJugador.AgregarVida(cantidadVida);
                    Debug.Log("Objeto de vida recogido, vida otorgada: " + cantidadVida);
                    Destroy(gameObject); // Destruye el objeto despu�s de otorgar vida
                }
                else
                {
                    Debug.Log("El jugador ya tiene la vida m�xima. No se otorga vida.");
                }
            }
        }
    }
}