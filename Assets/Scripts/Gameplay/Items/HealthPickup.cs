using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25;       // Vida que cura
    public float respawnTime = 10f;   // Tiempo hasta reaparecer

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
                player.Heal(healAmount);

            // Desactivar el ítem para que el spawner lo cuente como "muerto"
            gameObject.SetActive(false);

            // Respawn controlado por el spawner, no aquí
        }
    }
}
