using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    [SerializeField] private int daño = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Vida jugador = collision.GetComponent<Vida>();
            if (jugador != null)
            {
                jugador.RestarVida(daño);
            }
            Destroy(gameObject);
        }
    }
}