using UnityEngine;

public class EfectoSangre : MonoBehaviour
{
    [SerializeField] private ParticleSystem particulasSangre;

    public void EmitirSangre()
    {
        if (particulasSangre != null)
        {
            particulasSangre.Play();
        }
        else
        {
            Debug.LogWarning("No se asignó un sistema de partículas.");
        }
    }
}