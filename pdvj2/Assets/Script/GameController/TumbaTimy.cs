using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbaTimy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            MostrarMensaje();
        }
    }

    private void MostrarMensaje()
    {
        if (GameManager.Instance.MensajeTumbaTimy != null && GameManager.Instance.ImagenTumbaTimy != null)
        {
            GameManager.Instance.MensajeTumbaTimy.gameObject.SetActive(true);
            GameManager.Instance.ImagenTumbaTimy.gameObject.SetActive(true);

            // Espera 5 segundos antes de redirigir al men� principal
            Invoke(nameof(RedirigirAlMenuPrincipal), 5f);
        }
    }

    private void RedirigirAlMenuPrincipal()
    {
        // Limpia la progresi�n guardada
        GameManager.Instance.BorrarProgresion();

        // Redirige al men� principal
        GameManager.Instance.RegresarAlMenuPrincipal();
    }
}