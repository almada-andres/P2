using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mensajeHUD;
    [SerializeField] private float tiempoMensaje = 3f;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            MostrarMensaje();
        }
    }

    private void MostrarMensaje()
    {
        if (mensajeHUD != null)
        {
            mensajeHUD.gameObject.SetActive(true);

            CancelInvoke(nameof(OcultarMensaje));
            Invoke(nameof(OcultarMensaje), tiempoMensaje);
        }
    }
    private void OcultarMensaje()
    {
        if (mensajeHUD != null)
        {
            mensajeHUD.gameObject.SetActive(false);
        }
    } 
}
