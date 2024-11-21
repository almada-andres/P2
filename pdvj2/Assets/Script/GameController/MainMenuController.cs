using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;

    public void StartGame()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("Stage 1");
    }

    public void OpenOptions()
    {
        buttonClickSound.Play();
    }

    public void QuitGame()
    {
        buttonClickSound.Play(); 
        Application.Quit();
        Debug.Log("Juego cerrado"); // Solo para pruebas en el editor
    }
}