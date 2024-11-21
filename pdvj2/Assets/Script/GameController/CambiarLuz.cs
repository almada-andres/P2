using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AmbientarNocheOscura : MonoBehaviour
{
    [SerializeField] private Light2D luzGlobal;
    [SerializeField] private Color colorInicial = new Color(0.2f, 0.2f, 0.4f); // Color inicial (oscuro azul)
    [SerializeField] private Color colorFinal = new Color(0.05f, 0.05f, 0.1f); // Color final (mas oscuro)
    [SerializeField] private float duracionNoche = 30f; // Duracion de la transicion

    private float temporizador;
    private bool nocheIniciada = false;

    void Update()
    {
        if (nocheIniciada && temporizador > 0)
        {
            temporizador -= Time.deltaTime;

            // Suavizar la transicion del color
            luzGlobal.color = Color.Lerp(colorFinal, colorInicial, temporizador / duracionNoche);
        }
    }

    public void IniciarNoche()
    {
        // Restablecer el temporizador y empezar la transicion
        temporizador = duracionNoche;
        nocheIniciada = true;
        Debug.Log("Transición a noche iniciada");
    }
}
