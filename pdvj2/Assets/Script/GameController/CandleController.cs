using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CandleController : MonoBehaviour
{
    public GameObject candleLightObject; // Objeto que contiene la luz 2D de la vela
    public GameObject player; // Referencia al jugador
    public UnityEvent OnCandleCollected; // Evento para abrir la puerta

    private void OnEnable()
    {
        // Activa la luz cuando la vela aparece
        if (candleLightObject != null)
        {
            candleLightObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Debug.Log("Jugador tocó la vela");
            OnCandleCollected.Invoke(); // Llama al evento para abrir la puerta

            // Separa el objeto de luz y lo asigna al jugador
            if (candleLightObject != null && player != null)
            {
                candleLightObject.transform.SetParent(player.transform); // Asigna el objeto de luz al jugador
                candleLightObject.transform.localPosition = Vector3.zero; // Coloca la luz en el centro del jugador
            }

            // Desactiva el objeto de la vela (sin la luz)
            gameObject.SetActive(false);
        }
    }
}