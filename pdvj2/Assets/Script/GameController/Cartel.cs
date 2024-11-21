using UnityEngine;


public class Cartel : MonoBehaviour
{
    [SerializeField] private GameObject panelMensaje;

    private void Start()
    {
        panelMensaje.SetActive(false);   // Desactiva el panel del mensaje al inicio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            // Activa el panel del mensaje cuando el jugador entra en el trigger
            panelMensaje.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            // Desactiva el panel del mensaje cuando el jugador sale del trigger
            panelMensaje.SetActive(false);
        }
    }
}