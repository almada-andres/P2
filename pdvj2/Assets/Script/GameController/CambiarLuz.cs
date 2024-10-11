using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Necesario para Light2D

public class AmbientarNocheOscura : MonoBehaviour
{
    public Light2D luzGlobal;
    public Color colorInicial = new Color(0.2f, 0.2f, 0.4f); // Color inicial (oscuro azul)
    public Color colorFinal = new Color(0.05f, 0.05f, 0.1f); // Color final (m�s oscuro)
    public float duracionNoche = 30f;

    private float temporizador;
    private bool nocheIniciada = false;

    void Update()
    {
        if (nocheIniciada && temporizador > 0)
        {
            temporizador -= Time.deltaTime;

            // Suavizar la transici�n del color
            luzGlobal.color = Color.Lerp(colorFinal, colorInicial, temporizador / duracionNoche);
        }
    }

    public void IniciarNoche()
    {
        // Restablecer el temporizador y empezar la transici�n
        temporizador = duracionNoche;
        nocheIniciada = true;
        Debug.Log("Transici�n a noche iniciada");
    }
}