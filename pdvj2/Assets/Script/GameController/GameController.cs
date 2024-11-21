using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public void Victory()
    {
        Debug.Log("Has recogido todos los cofres.");
        //SceneManager.LoadScene("VictoryScene");
    }

    public void Defeat()
    {
        Debug.Log("¡Derrota! Te has quedado sin vida.");
        //SceneManager.LoadScene("GameOverScene");
    }
}