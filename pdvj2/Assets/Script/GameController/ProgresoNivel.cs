using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoProgresoNivel", menuName = "Progresion/ProgresoNivel")]
public class ProgresoNivelSO : ScriptableObject
{
    public int totalLlaves; // Cantidad total de llaves en el nivel
    public int totalCofres; // Cantidad total de cofres en el nivel

    public int llavesObtenidas; // Cantidad de llaves que el jugador ha obtenido
    public int cofresAbiertos;  // Cantidad de cofres que el jugador ha abierto

    public int vidaMaxima = 3;  // Vida máxima del jugador
    public int vidaActual;      // Vida actual del jugador

    // Método para resetear los valores (puedes llamarlo al inicio del nivel)
    public void ResetearProgreso()
    {
        llavesObtenidas = 0;
        cofresAbiertos = 0;
        vidaActual = vidaMaxima;
    }
}
