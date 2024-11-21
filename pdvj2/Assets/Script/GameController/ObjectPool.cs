using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Prefab que el pool va a administrar
    [SerializeField] private int initialSize = 10; // Tamaño del pool

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // Inicia el pool con objetos inactivos
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // Para obtener un objeto del pool
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Si el pool esta vacio, crea un nuevo objeto
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // Para devolver un objeto al pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}