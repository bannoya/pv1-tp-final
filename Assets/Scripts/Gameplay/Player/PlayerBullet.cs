using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;

        // 🔁 Rotar la bala para que apunte hacia la dirección de disparo
        if (direction.sqrMagnitude > 0.0001f)
        {
            // Si tu sprite de bala apunta a la DERECHA en el prefab:
            // usar transform.right
            transform.right = direction;

            // Alternativa con ángulo (equivalente):
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (rb != null)
        {
            // Usar la propiedad correcta de Rigidbody2D 
            rb.linearVelocity = direction * bulletSpeed;
        }

        // Destruir la bala después de lifeTime segundos para limpiar la escena 
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision?.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision?.gameObject);
    }

    private void HandleCollision(GameObject other)
    {
        if (other == null) return;

        // Ignorar colisiones con el jugador y con otras balas para evitar autodestrucción inmediata 
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet")) return;

        // Destruir si pega a enemigos conocidos 
        if (other.CompareTag("ZombieMele") || other.CompareTag("ZombieRange"))
        {
            Destroy(gameObject);
            return;
        }

        // Destruir en cualquier otra colisión que no sea explícitamente ignorada. 
        Destroy(gameObject);
    }
}

