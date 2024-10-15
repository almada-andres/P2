using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public AudioClip
 sonidoAbrirCofre;
    public ParticleSystem particulasCofre;
    private AudioSource audioSource;
    public GameController gameController;
    private int cofresRecogidos = 0;
    private Inventory inventory;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cofre"))
        {
            if (inventory.HasItem("llave"))
            {
                // Abrir el cofre
                audioSource.PlayOneShot(sonidoAbrirCofre);

                if (particulasCofre != null)
                {
                    particulasCofre.transform.position = collision.transform.position;
                    particulasCofre.Play();
                }

                cofresRecogidos++;

                if (cofresRecogidos >= gameController.totalCofres) // Comparar con el total de cofres en GameController
                {
                    Debug.Log("¡Victoria! Has recogido todos los cofres.");
                    gameController.Victory();
                }

                // Destruir el cofre después del tiempo más largo entre sonido y partículas
                float tiempoDestruccion = Mathf.Max(sonidoAbrirCofre.length, particulasCofre.main.duration);
                Destroy(collision.gameObject, tiempoDestruccion);

                inventory.RemoveItem("llave"); // Opcional: Eliminar la llave del inventario si solo se usa una vez
            }
            else
            {
                Debug.Log("Necesitas una llave para abrir el cofre");
            }
        }

        if (collision.CompareTag("Llave"))
        {
            inventory.AddItem("llave");
            Destroy(collision.gameObject);
        }
    }
}