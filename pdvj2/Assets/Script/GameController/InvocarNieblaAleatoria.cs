using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocarNieblaAleatoria : MonoBehaviour
{
    public GameObject prefabNiebla;  // Prefab de niebla
    public Vector2 rangoPosicion;     // Rango de posiciones para instanciar la niebla
    public float tiempoMin = 2f;      // Tiempo mínimo entre la aparición de la niebla
    public float tiempoMax = 6f;     // Tiempo máximo entre la aparición de la niebla
    public int maxNieblaEnEscena = 10; // Máxima cantidad de niebla permitida en la escena

    private float tiempoSiguienteNiebla;
    private List<GameObject> nieblasActivas = new List<GameObject>(); // Lista para almacenar nieblas activas

    void Start()
    {
        tiempoSiguienteNiebla = Random.Range(tiempoMin, tiempoMax);
    }

    void Update()
    {
        tiempoSiguienteNiebla -= Time.deltaTime;

        // Verifica si es tiempo de invocar más niebla
        if (tiempoSiguienteNiebla <= 0)
        {
            InvocarNiebla();
            tiempoSiguienteNiebla = Random.Range(tiempoMin, tiempoMax);
        }

        // Eliminar nieblas si se supera el límite
        LimitarNieblaEnEscena();
    }

    void InvocarNiebla()
    {
        // Crea una posición aleatoria dentro del rango definido
        Vector2 posicionAleatoria = new Vector2(Random.Range(-rangoPosicion.x, rangoPosicion.x),
                                                Random.Range(-rangoPosicion.y, rangoPosicion.y));
        // Instancia la niebla y la agrega a la lista
        GameObject niebla = Instantiate(prefabNiebla, posicionAleatoria, Quaternion.identity);
        nieblasActivas.Add(niebla);
    }

    void LimitarNieblaEnEscena()
    {
        // Verifica si hay más niebla activa que el límite permitido
        while (nieblasActivas.Count > maxNieblaEnEscena)
        {
            // Elimina la niebla más antigua
            GameObject nieblaARemover = nieblasActivas[0]; // Toma la niebla más antigua
            nieblasActivas.RemoveAt(0); // Elimina de la lista
            Destroy(nieblaARemover); // Destruye el objeto en la escena
        }
    }
}