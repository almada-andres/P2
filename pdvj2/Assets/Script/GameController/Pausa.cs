using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private AudioSource gameAudioSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Toggle muteToggle;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        volumeSlider.value = gameAudioSource.volume;

        resumeButton.onClick.AddListener(ResumeGame);
        quitGameButton.onClick.AddListener(QuitGame);

        muteToggle.onValueChanged.AddListener(ToggleMute);

        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuCanvas.activeSelf)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pausa el tiempo del juego
        pauseMenuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Restaura el tiempo del juego
        pauseMenuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); // Cierra la aplicación
    }

    public void ToggleMute(bool isMuted)
    {
        gameAudioSource.mute = isMuted; // Cambia el estado de silenciar según el valor del Toggle
    }

    public void AdjustVolume(float volume)
    {
        gameAudioSource.volume = volume; // Cambia el volumen del AudioSource
    }
}