using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivarPersistencia : MonoBehaviour
{
    private bool esPersistente = false;

    // Lista estática para mantener todos los objetos persistentes
    public static List<GameObject> objetosPersistentes = new List<GameObject>();

    private void Awake()
    {
        // Asegúrate de que este objeto es único al activarse la persistencia
        if (FindObjectsOfType<ActivarPersistencia>().Length > 1 && esPersistente)
        {
            Destroy(gameObject);
        }
    }

    public void HacerPersistente()
    {
        if (!esPersistente)
        {
            esPersistente = true;

            // Verifica si el objeto no es raíz
            if (transform.parent != null)
            {
                transform.SetParent(null); // Desanida el objeto para hacerlo raíz
            }

            // Agregar el objeto a la lista estática
            objetosPersistentes.Add(gameObject);

            DontDestroyOnLoad(gameObject); // Lo vuelve persistente
        }
    }
}