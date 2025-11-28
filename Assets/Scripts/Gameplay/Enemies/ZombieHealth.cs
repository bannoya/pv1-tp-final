using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHP = 500f;
    public float hp;
    public bool isAlive = true;
    [SerializeField] private Animator animator;
    private bool isDead = false;
    public PlayerStats playerStats;
    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        if (!isAlive) return;

        hp -= dmg;

        if (hp <= 0f)
        {
            hp = 0f;
            isAlive = false;
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // evita llamar dos veces
        if (playerStats != null)
            playerStats.zombieKills++;
        isDead = true;

        Debug.Log("Zombie muerto");

        // 1. Activar animación de muerte
        if (animator != null)
            animator.SetBool("isDead", true);

        // 2. Desactivar IA (movimiento, ataque, etc.)
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in scripts)
        {
            if (s != this)  // NO desactivar este script para poder destruir el objeto
                s.enabled = false;
        }

        // 3. Desactivar colisiones
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 4. Esperar a que termine la animación y destruir el objeto
        float deathAnimDuration = 1.2f; // AJUSTALO al tiempo real del clip “muerte”
        Destroy(gameObject, deathAnimDuration);
    }
}
