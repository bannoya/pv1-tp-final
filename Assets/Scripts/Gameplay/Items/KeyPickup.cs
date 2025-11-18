using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Buscamos el inventario del jugador
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.GetKey();   // ✅ Le damos la llave al jugador
            }
            else
            {
                Debug.LogWarning("El Player no tiene PlayerInventory agregado.");
            }

            Destroy(gameObject);   // destruimos la llave
        }
    }
}

