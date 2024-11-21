using UnityEngine;

[CreateAssetMenu(fileName = "NuevoProgresoNivel", menuName = "Progresion/ProgresoNivel")]
public class ProgresoNivelSO : ScriptableObject
{
    [SerializeField] private int totalLlaves; 
    [SerializeField] private int totalCofres; 

    [SerializeField] private int llavesObtenidas; 
    [SerializeField] private int cofresAbiertos; 

    [SerializeField] private int vidaMaxima = 3; 
    [SerializeField] private int vidaActual;  

    public int VidaActual
    {
        get { return vidaActual; }
        set { vidaActual = Mathf.Clamp(value, 0, vidaMaxima); } // Asegura que la vida no supere la vida máxima
    }

    public int VidaMaxima
    {
        get { return vidaMaxima; }
        set { vidaMaxima = Mathf.Max(0, value); } // Asegura que la vida máxima no sea menor a 0
    }

    public int GetTotalCofres()
    {
        return totalCofres;
    }

    public void SetTotalCofres(int value)
    {
        totalCofres = value;
    }

    public int GetCofresAbiertos()
    {
        return cofresAbiertos;
    }

    public void SetCofresAbiertos(int value)
    {
        cofresAbiertos = value;
    }

    public int TotalLlaves => totalLlaves;
    public int LlavesObtenidas => llavesObtenidas;

    public void ResetearProgreso()
    {
        llavesObtenidas = 0;
        cofresAbiertos = 0;
        vidaActual = vidaMaxima;
    }

    public void IncrementarLlaves()
    {
        llavesObtenidas++;
    }

    public void IncrementarCofres()
    {
        cofresAbiertos++;
    }

    public void ReducirVida(int cantidad)
    {
        VidaActual = Mathf.Max(0, vidaActual - cantidad); // Asegura que la vida no sea negativa
    }

    public void RestaurarVida()
    {
        VidaActual = vidaMaxima;
    }
}