using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class ControladorIntermitente : MonoBehaviour
{
    [SerializeField] private List<GameObject> objetos;
    [SerializeField] private float intervaloMin = 1f;
    [SerializeField] private float intervaloMax = 3f;

    private void Start()
    {
        // Comenzar el ciclo de activación aleatoria
        StartCoroutine(ControlarObjetosIntermitentes());
    }

    private IEnumerator ControlarObjetosIntermitentes()
    {
        while (true)
        {
            // Elegir un objeto aleatorio de la lista
            GameObject objetoSeleccionado = objetos[Random.Range(0, objetos.Count)];

            // Activar el objeto y su luz
            ActivarObjetoConLuz(objetoSeleccionado);

            // Esperar un tiempo aleatorio
            yield return new WaitForSeconds(Random.Range(intervaloMin, intervaloMax));

            // Desactivar el objeto y su luz
            DesactivarObjetoConLuz(objetoSeleccionado);
        }
    }

    private void ActivarObjetoConLuz(GameObject objeto)
    {
        objeto.SetActive(true);

        // Buscar el componente Light2D dentro del objeto y activarlo
        Light2D luz = objeto.GetComponentInChildren<Light2D>();
        if (luz != null)
        {
            luz.enabled = true;
        }
    }

    private void DesactivarObjetoConLuz(GameObject objeto)
    {
        // Desactivar la luz del objeto
        Light2D luz = objeto.GetComponentInChildren<Light2D>();
        if (luz != null)
        {
            luz.enabled = false;
        }

        // Desactivar el objeto
        objeto.SetActive(false);
    }
}