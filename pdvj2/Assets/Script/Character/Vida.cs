using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RestarVida(int da�o)
    {
        vidaActual -= da�o;
        
        Debug.Log("Vida restante: " + vidaActual);
    }
}
