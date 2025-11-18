using UnityEngine;

public class KeyPickup : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           

            // Desactivar el ítem para que el spawner lo cuente como "muerto"
            Destroy(gameObject);


        }
    }
}
