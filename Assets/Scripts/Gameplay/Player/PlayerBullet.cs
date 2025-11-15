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

        if (rb != null)
        {
            // Usar la propiedad correcta de Rigidbody2D 
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            // Fallback si no hay Rigidbody2D: mover con Transform 
            // Esto permite que la bala se mueva aunque falte el componente en la escena 
            StartCoroutine(MoveWithoutRigidbody());
        }

        // Destruir la bala después de lifeTime segundos para limpiar la escena 
        Destroy(gameObject, lifeTime);
    }

    private System.Collections.IEnumerator MoveWithoutRigidbody()
    {
        float timer = 0f;
        while (timer < lifeTime)
        {
            transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
            timer += Time.deltaTime;
            yield return null;
        }
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
