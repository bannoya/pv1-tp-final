using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25;       
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
                player.Heal(healAmount);

            // Desactivar el ítem para que el spawner lo cuente como "muerto"
            gameObject.SetActive(false);

        }
    }
}
