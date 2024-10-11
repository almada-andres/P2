using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocarNieblaAleatoria : MonoBehaviour
{
    public GameObject prefabNiebla;  // Prefab de niebla
    public Vector2 rangoPosicion;     // Rango de posiciones para instanciar la niebla
    public float tiempoMin = 2f;      // Tiempo m�nimo entre la aparici�n de la niebla
    public float tiempoMax = 6f;     // Tiempo m�ximo entre la aparici�n de la niebla
    public int maxNieblaEnEscena = 10; // M�xima cantidad de niebla permitida en la escena

    private float tiempoSiguienteNiebla;
    private List<GameObject> nieblasActivas = new List<GameObject>(); // Lista para almacenar nieblas activas

    void Start()
    {
        tiempoSiguienteNiebla = Random.Range(tiempoMin, tiempoMax);
    }

    void Update()
    {
        tiempoSiguienteNiebla -= Time.deltaTime;

        // Verifica si es tiempo de invocar m�s niebla
        if (tiempoSiguienteNiebla <= 0)
        {
            InvocarNiebla();
            tiempoSiguienteNiebla = Random.Range(tiempoMin, tiempoMax);
        }

        // Eliminar nieblas si se supera el l�mite
        LimitarNieblaEnEscena();
    }

    void InvocarNiebla()
    {
        // Crea una posici�n aleatoria dentro del rango definido
        Vector2 posicionAleatoria = new Vector2(Random.Range(-rangoPosicion.x, rangoPosicion.x),
                                                Random.Range(-rangoPosicion.y, rangoPosicion.y));
        // Instancia la niebla y la agrega a la lista
        GameObject niebla = Instantiate(prefabNiebla, posicionAleatoria, Quaternion.identity);
        nieblasActivas.Add(niebla);
    }

    void LimitarNieblaEnEscena()
    {
        // Verifica si hay m�s niebla activa que el l�mite permitido
        while (nieblasActivas.Count > maxNieblaEnEscena)
        {
            // Elimina la niebla m�s antigua
            GameObject nieblaARemover = nieblasActivas[0]; // Toma la niebla m�s antigua
            nieblasActivas.RemoveAt(0); // Elimina de la lista
            Destroy(nieblaARemover); // Destruye el objeto en la escena
        }
    }
}