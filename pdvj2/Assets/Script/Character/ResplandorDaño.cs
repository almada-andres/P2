using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class Resplandor : MonoBehaviour
{
    public Light2D luzResplandor;
    public float intensidadInicial = 1.5f;
    public float duracionResplandor = 0.5f;
    private bool enResplandor = false; 

    void Start()
    {
        if (luzResplandor == null)
        {
            Debug.LogError("No se asignó una Light2D al script Resplandor.");
            enabled = false; // Desactiva el script
        }

        luzResplandor.intensity = 0;
    }

    public void Luz()
    {
        if (!enResplandor)
        {
            StartCoroutine(ActivarResplandor());
        }
    }

    private IEnumerator ActivarResplandor()
    {
        enResplandor = true;

        if (luzResplandor != null)
        {
            luzResplandor.intensity = intensidadInicial;
        }

        yield return new WaitForSeconds(duracionResplandor);

        if (luzResplandor != null)
        {
            luzResplandor.intensity = 0;
        }

        enResplandor = false;
    }
}
