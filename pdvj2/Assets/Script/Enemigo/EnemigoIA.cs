using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemigo : MonoBehaviour
{
    // Variables a configurar desde el editor
       [Header("Configuracion")]
       [SerializeField] float velocidad = 5f;
    [SerializeField]
    float limiteIzquierdo;
    [SerializeField] float limiteDerecho;

    // Variables de uso interno en el script
    private float direccion = 1f; // 1 para derecha, -1 para izquierda

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable) � 

    private void FixedUpdate()
    {
        // Mover el enemigo
        miRigidbody2D.velocity = new Vector2(direccion * velocidad, 0);

        // Verificar si ha alcanzado los l�mites
        if (transform.position.x >= limiteDerecho)
        {
            direccion = -1; // Cambiar direcci�n a la izquierda
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            direccion = 1; // Cambiar direcci�n a la derecha
        }
    }
}