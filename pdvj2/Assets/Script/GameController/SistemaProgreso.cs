using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaProgreso : MonoBehaviour
{
    public ProgresoNivelSO progresoNivel; // Referencia al Scriptable Object

    private void Start()
    {
        // Resetea el progreso al inicio del nivel
        progresoNivel.ResetearProgreso();
    }

    // Método para añadir una llave obtenida
    public void ObtenerLlave()
    {
        progresoNivel.llavesObtenidas++;

        // Verifica si el jugador obtuvo todas las llaves
        if (progresoNivel.llavesObtenidas >= progresoNivel.totalLlaves)
        {
            Debug.Log("¡Has obtenido todas las llaves!");
        }
    }

    // Método para abrir un cofre
    public void AbrirCofre()
    {
        progresoNivel.cofresAbiertos++;

        // Verifica si el jugador abrio todos los cofres
        if (progresoNivel.cofresAbiertos >= progresoNivel.totalCofres)
        {
            Debug.Log("¡Has abierto todos los cofres!");
        }
    }
}