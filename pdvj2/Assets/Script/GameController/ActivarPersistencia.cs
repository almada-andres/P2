using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivarPersistencia : MonoBehaviour
{
    private bool esPersistente = false;

    // Lista est�tica para mantener todos los objetos persistentes
    public static List<GameObject> objetosPersistentes = new List<GameObject>();

    private void Awake()
    {
        // Aseg�rate de que este objeto es �nico al activarse la persistencia
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

            // Verifica si el objeto no es ra�z
            if (transform.parent != null)
            {
                transform.SetParent(null); // Desanida el objeto para hacerlo ra�z
            }

            // Agregar el objeto a la lista est�tica
            objetosPersistentes.Add(gameObject);

            DontDestroyOnLoad(gameObject); // Lo vuelve persistente
        }
    }
}