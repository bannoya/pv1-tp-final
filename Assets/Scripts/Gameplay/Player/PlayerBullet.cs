using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 3f;
    private float damage = 100f;
    [Header("Animación de destrucción")]
    [SerializeField] private Animator animator;      // Animator de la bala
    [SerializeField] private float destroyDelay = 0.2f; // Tiempo para que se vea la animación

    private Vector2 direction;
    private Rigidbody2D rb;
    private bool isDestroying = false;              // Para no ejecutar destrucción 2 veces

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Por si te olvidás de arrastrar el Animator en el inspector
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Init(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;

        // 🔁 Rotar la bala para que apunte hacia la dirección de disparo
        if (direction.sqrMagnitude > 0.0001f)
        {
            // Si tu sprite de bala apunta a la DERECHA en el prefab:
            transform.right = direction;

            // Alternativa con ángulo:
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        // Destruir la bala después de lifeTime segundos si no chocó antes
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
        if (other == null || isDestroying) return;

        // Ignorar colisiones con el jugador y con otras balas
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet")) return;

        // Si pega a enemigos conocidos o cualquier otra cosa:
        ZombieHealth zombie = other.GetComponent<ZombieHealth>();
        BossHealth boss = other.GetComponent<BossHealth>();
        if (zombie != null)
        {    
            zombie.TakeDamage(damage);
        }
        else if (boss != null)
        {
            boss.TakeDamage(damage);
        }
        StartDestroySequence();
    }

    private void StartDestroySequence()
    {
        isDestroying = true;

        // Frenar la bala
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Desactivar el collider para que no siga pegando cosas
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Disparar animación de destrucción
        if (animator != null)
            animator.SetBool("isDestroying", true);

        // Destruir después de que se vea la animación
        Destroy(gameObject, destroyDelay);
    }
}


