using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 10f; // opcional, si quieres hacer daño
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // destruir tras unos segundos por seguridad
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si la bala toca un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Opcional: si el enemigo tiene un script de vida, puedes restarle HP aquí
            // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            Destroy(gameObject); // destruir la bala
        }
    }
}
