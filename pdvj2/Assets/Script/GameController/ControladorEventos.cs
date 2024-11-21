using UnityEngine;

public class ControladorEventos : MonoBehaviour
{
    [SerializeField] private float tiempoEvento1 = 10f; // Tiempo en segundos para el primer evento
    [SerializeField] private float tiempoEvento2 = 20f; // Tiempo en segundos para el segundo evento
    [SerializeField] private AmbientarNocheOscura ambientarNoche; // Referencia al script que controla la noche

    private float temporizador = 0f;   // Temporizador para contar el tiempo transcurrido

    void Update()
    {
        // Sumar el tiempo transcurrido
        temporizador += Time.deltaTime;

        // Verificar si es el momento de ejecutar el primer evento
        if (temporizador >= tiempoEvento1)
        {
            EjecutarEvento1();
            tiempoEvento1 = Mathf.Infinity; // Evitar que se ejecute nuevamente
        }

        // Verificar si es el momento de ejecutar el segundo evento
        if (temporizador >= tiempoEvento2)
        {
            EjecutarEvento2();
            tiempoEvento2 = Mathf.Infinity; // Evitar que se ejecute nuevamente
        }
    }

    void EjecutarEvento1()
    {
        Debug.Log("Ejecutando Evento 1");
        ambientarNoche.IniciarNoche();
    }

    void EjecutarEvento2()
    {
        Debug.Log("Ejecutando Evento 2");
    }
}