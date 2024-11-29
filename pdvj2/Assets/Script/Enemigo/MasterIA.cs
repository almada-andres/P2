using UnityEngine;

public class Master : MonoBehaviour
{
    [SerializeField] private float rangoVisionInicial = 5f;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private Transform posicionInicial;
    [SerializeField] private Transform jugador;
    [SerializeField] private Vector3 escalaInicial = Vector3.one;
    [SerializeField] private Vector3 escalaPersecucion = Vector3.one * 1.5f;

    private Jugador jugadorScript; // Referencia al script del jugador para verificar si es visible
    private bool persiguiendo = false;
    private bool regresando = false; // Indica si el enemigo está regresando a su posición inicial
    private float rangoVisionActual; 
    private Vector3 posicionOriginal; // Guarda la posición inicial

    void Start()
    {
        // Configurar el rango de visión inicial y guardar la posición original
        rangoVisionActual = rangoVisionInicial;
        posicionOriginal = transform.position;

        // Configurar la escala inicial del enemigo
        transform.localScale = escalaInicial;

        // Validar referencias
        if (jugador == null)
        {
            Debug.LogError("Falta asignar el jugador en el script del enemigo.");
        }
        else
        {
            jugadorScript = jugador.GetComponent<Jugador>();
            if (jugadorScript == null)
            {
                Debug.LogError("El jugador no tiene un script 'Jugador' asociado.");
            }
        }

        if (posicionInicial != null)
        {
            posicionOriginal = posicionInicial.position;
        }
    }

    void Update()
    {
        if (jugador == null || jugadorScript == null)
            return;

        // Calcular la distancia al jugador
        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);

        if (!persiguiendo && distanciaJugador <= rangoVisionActual && jugadorScript.EsVisible())
        {
            // Si el jugador entra en el rango de visión y es visible
            ComenzarPersecucion();
        }
        else if (persiguiendo && (!jugadorScript.EsVisible() || distanciaJugador > rangoVisionActual))
        {
            // Si el jugador deja de ser visible o sale del rango
            RegresarAPosicionInicial();
        }

        if (persiguiendo)
        {
            PerseguirJugador();
        }
        else if (regresando)
        {
            RegresarAPosicion();
        }
    }

    private void ComenzarPersecucion()
    {
        persiguiendo = true;
        regresando = false;

        // Incrementar el rango de visión
        rangoVisionActual = rangoVisionInicial + (rangoVisionInicial / 2);

        // Cambiar la escala del enemigo
        transform.localScale = escalaPersecucion;
    }

    private void RegresarAPosicionInicial()
    {
        persiguiendo = false;
        regresando = true;

        // Restaurar el rango de visión inicial
        rangoVisionActual = rangoVisionInicial;
    }

    private void PerseguirJugador()
    {
        // Moverse hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    private void RegresarAPosicion()
    {
        // Moverse hacia la posición inicial
        Vector3 direccion = (posicionOriginal - transform.position).normalized;
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;

        // Si llegó a la posición inicial, detener el regreso
        if (Vector3.Distance(transform.position, posicionOriginal) < 0.1f)
        {
            regresando = false;
            transform.localScale = escalaInicial; // Restaurar la escala inicial
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar el rango de visión en la escena para depuración
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVisionActual);
    }
}