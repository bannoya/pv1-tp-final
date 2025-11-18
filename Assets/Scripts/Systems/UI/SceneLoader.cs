using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string cargarEscena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log($"Cargando escena: {cargarEscena}");
                SceneManager.LoadScene(cargarEscena);
            }
            else
            {
                Debug.Log("No podés pasar, te falta la llave 🔒");
            }
        }
    }
}
