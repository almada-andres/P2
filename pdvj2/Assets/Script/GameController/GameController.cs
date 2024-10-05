using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Para cargar y reiniciar escenas
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void Victory()
    {
        Debug.Log("¡Victoria! Has recogido todos los cofres.");
        // Cargar la escena de victoria o mostrar un panel de victoria
        SceneManager.LoadScene("VictoryScene");
    }

    public void Defeat()
    {
        Debug.Log("¡Derrota! Te has quedado sin vida.");
        // Cargar la escena de derrota o mostrar un panel de Game Over
        SceneManager.LoadScene("GameOverScene"); 
    }
}
