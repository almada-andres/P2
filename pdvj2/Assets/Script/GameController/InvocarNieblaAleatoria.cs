using UnityEngine;

public class InvocarNieblaAleatoria : MonoBehaviour
{
    [SerializeField] private ObjectPool poolNiebla;
    [SerializeField] private Vector2 rangoPosicion; 
    [SerializeField] private float tiempoMin = 2f; 
    [SerializeField] private float tiempoMax = 6f; 
    [SerializeField] private int maxNieblaEnEscena = 10;
    [SerializeField] private int minimoNieblaActiva = 5;

    private float tiempoProximaNiebla;
    private int nieblasActivas = 0; // Contador de nieblas activas

    void Start()
    {
        // Generar las nieblas minimas al inicio
        for (int i = 0; i < minimoNieblaActiva; i++)
        {
            InvocarNiebla();
        }

        // Configura el tiempo para la proxima niebla
        tiempoProximaNiebla = Random.Range(tiempoMin, tiempoMax);
    }

    void Update()
    {
        // Garantiza que haya nieblas activas
        while (nieblasActivas < minimoNieblaActiva)
        {
            InvocarNiebla();
        }

        // Controlar la invocacion de nieblas adicionales basadas en tiempo
        tiempoProximaNiebla -= Time.deltaTime;
        if (tiempoProximaNiebla <= 0 && nieblasActivas < maxNieblaEnEscena)
        {
            InvocarNiebla();
            tiempoProximaNiebla = Random.Range(tiempoMin, tiempoMax);
        }
    }

    void InvocarNiebla()
    {
        // Genera una posicion aleatoria dentro del rango definido
        Vector2 posicionAleatoria = new Vector2(Random.Range(-rangoPosicion.x, rangoPosicion.x),
                                                Random.Range(-rangoPosicion.y, rangoPosicion.y));
        // Obtiene un objeto del pool y lo coloca en la posicion
        GameObject niebla = poolNiebla.GetObject();
        niebla.transform.position = posicionAleatoria;
        nieblasActivas++;

        // Devuelve la niebla al pool despues de un tiempo
        StartCoroutine(DevolverNiebla(niebla, 5f)); // la niebla dura 5 segundos
    }

    System.Collections.IEnumerator DevolverNiebla(GameObject niebla, float delay)
    {
        yield return new WaitForSeconds(delay);
        poolNiebla.ReturnObject(niebla);
        nieblasActivas--;
    }
}