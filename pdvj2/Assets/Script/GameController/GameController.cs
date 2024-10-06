using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Para cargar y reiniciar escenas
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int totalCofres = 1; // Valor por defecto, puedes cambiarlo en el Inspector

    public void Victory()
    {
        Debug.Log("¡Victoria! Has recogido todos los cofres.");
        SceneManager.LoadScene("VictoryScene");
    }

    public void Defeat()
    {
        Debug.Log("¡Derrota! Te has quedado sin vida.");
        SceneManager.LoadScene("GameOverScene");
    }
}
