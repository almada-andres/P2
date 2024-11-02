using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // Necesario para obtener el nombre de la escena
using TMPro;

public class SistemaProgreso : MonoBehaviour
{
    public ProgresoNivelSO progresoNivel; // Referencia al Scriptable Object
    public UnityEvent OnAllChestsOpened; // Evento para cuando se abran todos los cofres

    [Header("UI")]
    public TextMeshProUGUI nivelText; // Referencia al TextMeshPro para mostrar el nombre del nivel

    private void Start()
    {
        // Resetea el progreso al inicio del nivel
        progresoNivel.ResetearProgreso();

        // Actualiza el nombre del nivel con el nombre de la escena
        ActualizarNombreNivel();
    }

    private void ActualizarNombreNivel()
    {
        if (nivelText != null)
        {
            // Asigna el nombre de la escena actual al TextMeshPro
            nivelText.text = SceneManager.GetActiveScene().name;
        }
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

        // Verifica si el jugador abrió todos los cofres
        if (progresoNivel.cofresAbiertos >= progresoNivel.totalCofres)
        {
            Debug.Log("¡Has abierto todos los cofres!");
            OnAllChestsOpened.Invoke(); // Activa el evento para que aparezca la vela
        }
    }

    // Método para avanzar al siguiente nivel
    public void AvanzarNivel()
    {
        // Actualiza el nombre en pantalla al cargar una nueva escena
        ActualizarNombreNivel();
    }
}