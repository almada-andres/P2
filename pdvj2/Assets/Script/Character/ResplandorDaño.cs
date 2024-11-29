using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Resplandor : MonoBehaviour
{
    [SerializeField] private Light2D luzResplandor; 
    [SerializeField] private float intensidadInicial = 1.5f;
    [SerializeField] private float duracionResplandor = 0.5f;
    private bool enResplandor = false;

    void Start()
    {
        if (luzResplandor == null)
        {
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