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

    public void RestarVida(int daño)
    {
        vidaActual -= daño;
        
        Debug.Log("Vida restante: " + vidaActual);
    }
}
