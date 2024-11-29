using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Configuración del Enemigo")]
    public float velocidad = 2f;
    public float rangoVision = 1.5f;

    [Header("Límites para Patrullaje")]
    [SerializeField] private float limiteIzquierdo;
    [SerializeField] private float limiteDerecho;
    [SerializeField] private float limiteSuperior;
    [SerializeField] private float limiteInferior;

    private Rigidbody2D rb;
    private Jugador jugador;

    private Vector2 direccion;
    private bool persiguiendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindWithTag("Jugador").GetComponent<Jugador>();

        // Determina la direccion inicial (horizontal o vertical)
        if (limiteIzquierdo != limiteDerecho)
            direccion = Vector2.right; // Patrullaje horizontal
        else if (limiteSuperior != limiteInferior)
            direccion = Vector2.up; // Patrullaje vertical
        else
        {
            direccion = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.transform.position);

            if (distanciaAlJugador <= rangoVision && jugador.EsVisible())
            {
                if (!persiguiendo)
                {
                    persiguiendo = true;
                }
                PerseguirJugador();
            }
            else
            {
                if (persiguiendo)
                {
                    persiguiendo = false;
                }
                Patrullar();
            }
        }
        else
        {
            Patrullar();
        }
    }

    private void PerseguirJugador()
    {
        // Dirección hacia el jugador
        Vector2 direccionJugador = (jugador.transform.position - transform.position).normalized;
        rb.velocity = direccionJugador * velocidad;
    }

    private void Patrullar()
    {
        // Patrullaje horizontal
        if (direccion == Vector2.right || direccion == Vector2.left)
        {
            if (transform.position.x >= limiteDerecho)
                direccion = Vector2.left;
            else if (transform.position.x <= limiteIzquierdo)
                direccion = Vector2.right;

            rb.velocity = new Vector2(direccion.x * velocidad, 0);
        }
        // Patrullaje vertical
        else if (direccion == Vector2.up || direccion == Vector2.down)
        {
            if (transform.position.y >= limiteSuperior)
                direccion = Vector2.down;
            else if (transform.position.y <= limiteInferior)
                direccion = Vector2.up;

            rb.velocity = new Vector2(0, direccion.y * velocidad);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Para ver el rango de vision del enemigo en Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVision);

        // Para visualizar el recorrido de patrullaje en Unity
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(limiteIzquierdo, limiteInferior), new Vector2(limiteDerecho, limiteInferior));
        Gizmos.DrawLine(new Vector2(limiteDerecho, limiteInferior), new Vector2(limiteDerecho, limiteSuperior));
        Gizmos.DrawLine(new Vector2(limiteDerecho, limiteSuperior), new Vector2(limiteIzquierdo, limiteSuperior));
        Gizmos.DrawLine(new Vector2(limiteIzquierdo, limiteSuperior), new Vector2(limiteIzquierdo, limiteInferior));
    }
}