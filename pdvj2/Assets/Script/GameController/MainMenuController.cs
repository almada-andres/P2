using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;
    [SerializeField] private GameObject continuarPartidaButton;

    void Start()
    {
        // Cargar progreso al iniciar el menú principal
        GameManager.Instance.CargarProgresion();

        // Habilitar el botón de continuar si hay progreso guardado
        continuarPartidaButton.SetActive(GameManager.Instance.nivelActual > 1);
    }

    public void StartGame()
    {
        buttonClickSound.Play();
        // Mostrar el submenú para elegir entre Nueva Partida o Continuar
        continuarPartidaButton.SetActive(true);
    }

    public void ContinueGame()
    {
        buttonClickSound.Play();

        // Cargar el nivel guardado
        int nivelGuardado = GameManager.Instance.nivelActual;
        if (nivelGuardado > 1)
        {
            SceneManager.LoadScene($"Stage {nivelGuardado}");
        }
        else
        {
            Debug.LogWarning("No hay progreso guardado. Inicia una nueva partida.");
        }
    }

    public void NewGame()
    {
        buttonClickSound.Play();

        // Reiniciar el progreso y cargar el primer nivel
        GameManager.Instance.ReiniciarJuego();
    }

    public void BackToMainMenu()
    {
        buttonClickSound.Play();
        // Volver al estado inicial del menú
        continuarPartidaButton.SetActive(false);
    }

    public void OpenOptions()
    {
        buttonClickSound.Play();
        // Aquí puedes implementar las opciones si es necesario
    }

    public void QuitGame()
    {
        buttonClickSound.Play();
        Application.Quit();
    }
}