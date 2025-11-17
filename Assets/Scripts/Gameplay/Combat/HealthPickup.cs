using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25;       // Cuánta vida cura
    public float respawnTime = 10f;   // Tiempo de respawn
    public Transform[] spawnPoints;   // Lista de puntos donde puede aparecer

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
                player.Heal(healAmount);

            // Ocultar el ítem luego de agarrarlo
            gameObject.SetActive(false);

            // Llamar al respawn luego de X segundos
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    void Respawn()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("HealthPickup no tiene puntos de spawn asignados.");
            return;
        }

        // Elegir un punto random
        int index = Random.Range(0, spawnPoints.Length);

        // Mover el ítem a ese punto
        transform.position = spawnPoints[index].position;

        // Activarlo nuevamente
        gameObject.SetActive(true);
    }
}
