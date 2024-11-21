using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSangre : MonoBehaviour
{
    public ParticleSystem particulasSangre;

    public void EmitirSangre()
    {
        if (particulasSangre != null)
        {
            particulasSangre.Play();
        }
        else
        {
            Debug.LogWarning("No se asign� un sistema de part�culas.");
        }
    }
}