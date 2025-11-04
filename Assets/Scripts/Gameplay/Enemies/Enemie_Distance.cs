using UnityEngine;

public class Enemie_Distance : MonoBehaviour

{
    [Header("Movimiento")]
    public float speed = 2.0f;
    public float chaseRange = 6f;

    [Header("Vida")]
    public int maxHP = 3;
    int hp;

    Transform player;

    void Awake()
    {
        hp = maxHP;
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= chaseRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    void Die()
    {
        // TODO: anim muerte, drop, etc.
        Destroy(gameObject);
    }

    // Si usás Trigger para hacer daño al jugador:
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: aplicar daño al jugador
        }
    }
}

