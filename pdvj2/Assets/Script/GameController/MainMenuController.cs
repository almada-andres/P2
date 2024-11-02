using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource buttonClickSound; // AudioSource para el sonido de clic
    public void StartGame()
    {
        buttonClickSound.Play(); // Sonido al presionar el boton
        SceneManager.LoadScene("Stage 1");
    }

    public void OpenOptions()
    {
        buttonClickSound.Play(); // sonido al presionar el boton
    }

    public void QuitGame()
    {
        buttonClickSound.Play(); // Sonido al presionar el boton
        Application.Quit();
        Debug.Log("Juego cerrado"); // Solo para pruebas en el editor
    }
}