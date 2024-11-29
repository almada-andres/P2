using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI MensajeTumbaTimy { get; private set; }
    public Image ImagenTumbaTimy { get; private set; }

    public int nivelActual = 1;
    public int llavesRecogidas = 0;
    public int vidaJugador = 3;

    [SerializeField] private GameObject gameOverHUD;
    [SerializeField] private float tiempoParaMenuPrincipal = 10f;
    [SerializeField] private ActivarPersistencia[] objetosPersistentes;

    private string rutaArchivo;
    private AudioSource musicaFondo;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (transform.parent != null)
        {
            transform.SetParent(null); // Desanida el objeto para hacerlo raíz
        }
        DontDestroyOnLoad(gameObject);

        // Define la ruta para guardar el archivo JSON
        rutaArchivo = Path.Combine(Application.persistentDataPath, "progresion.json");

        musicaFondo = GameObject.Find("MusicaFondo").GetComponent<AudioSource>();
    }

    public void RegistrarUI(TextMeshProUGUI mensaje, Image imagen)
    {
        MensajeTumbaTimy = mensaje;
        ImagenTumbaTimy = imagen;
    }

    // --------- Gestión del progreso del juego ----------
    public void GuardarProgresion()
    {
        ProgresionDatos datos = new ProgresionDatos
        {
            nivelActual = nivelActual,
            llavesRecogidas = llavesRecogidas,
            vidaJugador = vidaJugador
        };

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Progresión guardada en: " + rutaArchivo);
    }

    public void CargarProgresion()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            ProgresionDatos datos = JsonUtility.FromJson<ProgresionDatos>(json);

            nivelActual = datos.nivelActual;
            llavesRecogidas = datos.llavesRecogidas;
            vidaJugador = datos.vidaJugador;

            Debug.Log("Progresión cargada.");
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de progresión. Inicia una nueva partida.");
        }
    }

    public void ReiniciarJuego()
    {
        nivelActual = 1;
        llavesRecogidas = 0;
        vidaJugador = 3; // Vida inicial

        GuardarProgresion();
        SceneManager.LoadScene("Stage 1");
    }

    // --------- Gestión de victoria y derrota ----------
    public void Victory(string siguienteNivel)
    {
        foreach (var objeto in objetosPersistentes)
        {
            objeto.HacerPersistente();
        }

        // Cargar siguiente nivel
        SceneManager.LoadScene(siguienteNivel);
    }

    public void Defeat()
    {
        ActivarGameOverHUD();
        Invoke(nameof(RegresarAlMenuPrincipal), tiempoParaMenuPrincipal);
    }

    private void ActivarGameOverHUD()
    {
        if (gameOverHUD != null)
        {
            gameOverHUD.SetActive(true); // Muestra el panel o imagen de Game Over
        }
    }

    public void RegresarAlMenuPrincipal()
    {
        // Destruir todos los objetos persistentes que no sean el GameManager
        foreach (GameObject objeto in ActivarPersistencia.objetosPersistentes)
        {
            // Asegurarse de no destruir el GameManager
            if (objeto != GameManager.Instance.gameObject)
            {
                Destroy(objeto);
            }
        }

        // Limpiar la lista de objetos persistentes para asegurarnos de que no queden en memoria
        ActivarPersistencia.objetosPersistentes.Clear();

        SceneManager.LoadScene("Menu Principal");
    }

    public void BorrarProgresion()
    {
        // Restaura los valores iniciales del progreso
        nivelActual = 1;
        llavesRecogidas = 0;
        vidaJugador = 3;

        // Elimina el archivo de progresión si existe
        if (File.Exists(rutaArchivo))
        {
            File.Delete(rutaArchivo);
            Debug.Log("Progresión eliminada.");
        }
    }
}

[System.Serializable]
public class ProgresionDatos
{
    public int nivelActual;
    public int llavesRecogidas;
    public int vidaJugador;
}