using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    public string cargarEscena;
    // Esta función se llama AUTOMÁTICAMENTE
    // cuando algo entra en el "Trigger"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Revisamos si lo que entró es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Cargando escena: {cargarEscena}");

            // La línea que carga el nivel
            SceneManager.LoadScene(cargarEscena);
        }
    }
}