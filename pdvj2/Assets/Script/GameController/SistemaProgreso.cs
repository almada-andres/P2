using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SistemaProgreso : MonoBehaviour
{
    [Header("Progreso del Nivel")]
    public ProgresoNivelSO progresoNivel;
    public UnityEvent OnAllChestsOpened; // Evento para cuando se abran todos los cofres

    [Header("UI")]
    public TextMeshProUGUI nivelText; // Referencia al TextMeshPro para mostrar el nombre del nivel

    private void Start()
    {
        // Resetea el progreso al inicio del nivel
        progresoNivel.ResetearProgreso();

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

    public void ObtenerLlave()
    {
        progresoNivel.IncrementarLlaves();

        // Verifica si el jugador obtuvo todas las llaves
        if (progresoNivel.LlavesObtenidas >= progresoNivel.TotalLlaves)
        {
            Debug.Log("¡Has obtenido todas las llaves!");
        }
    }

    public void AbrirCofre()
    {
        progresoNivel.IncrementarCofres();

        // Verifica si el jugador abrió todos los cofres
        if (progresoNivel.GetCofresAbiertos() >= progresoNivel.GetTotalCofres())
        {
            Debug.Log("¡Has abierto todos los cofres!");
            OnAllChestsOpened.Invoke(); // Activa el evento para que aparezca la vela
        }
    }

    // Metodo para avanzar al siguiente nivel
    public void AvanzarNivel()
    {
        // Actualiza el nombre en pantalla al cargar una nueva escena
        ActualizarNombreNivel();
        // Lógica para cargar la siguiente escena si es necesario
        // SceneManager.LoadScene("NextLevel");
    }
}