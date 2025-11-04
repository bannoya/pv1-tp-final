using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Distance_IA : MonoBehaviour
{
    [Header("Objetivo")]
    public string playerTag = "Player";
    private Transform player;

    [Header("Rangos")]
    public float chaseRange = 10f;       // empieza a perseguir
    public float desiredDistance = 3f;   // distancia ideal a mantener
    public float disengageRange = 14f;   // deja de perseguir si el player se va más lejos

    [Header("Movimiento")]
    public float moveSpeed = 2f;
    public bool usePhysicsMove = true;

    [Header("Ataque a distancia")]
    public GameObject bulletPrefab;      // asignar prefab de bala
    public Transform firePoint;          // empty hijo desde donde dispara
    public float fireCooldown = 1.5f;    // tiempo entre disparos
    private float fireTimer;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var p = GameObject.FindGameObjectWithTag(playerTag);
        if (p) player = p.transform;

        // recomendado para top-down
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!player) return;

        float d = Vector2.Distance(transform.position, player.position);
        fireTimer -= Time.deltaTime;

        // muy lejos: detener
        if (d > disengageRange)
        {
            velocity = Vector2.zero;
            return;
        }

        if (d <= chaseRange)
        {
            Vector2 dirToPlayer = (player.position - transform.position).normalized;

            if (d > desiredDistance)                    // acercarse
            {
                velocity = dirToPlayer * moveSpeed;
            }
            else if (d < desiredDistance * 0.9f)        // alejarse (histéresis 10%)
            {
                velocity = -dirToPlayer * moveSpeed;
            }
            else                                         // en la banda ideal → disparar
            {
                velocity = Vector2.zero;

                if (fireTimer <= 0f && bulletPrefab && firePoint)
                {
                    Shoot(dirToPlayer);
                    fireTimer = fireCooldown;
                }
            }
        }
        else
        {
            velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (usePhysicsMove)
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        else
            transform.position += (Vector3)(velocity * Time.fixedDeltaTime);
    }

    void Shoot(Vector2 dir)
    {
        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var comp = b.GetComponent<Enemie_Bullet>();
        if (comp) comp.SetDirection(dir);
    }

    // Gizmos para debug
    void OnDrawGizmosSelected()
    {
        DrawCircle(Color.yellow, chaseRange);
        DrawCircle(Color.green, desiredDistance);
        DrawCircle(Color.gray, disengageRange);
    }
    void DrawCircle(Color c, float r)
    {
        Gizmos.color = c;
        const int seg = 32;
        Vector3 center = transform.position;
        Vector3 prev = center + new Vector3(r, 0, 0);
        for (int i = 1; i <= seg; i++)
        {
            float a = i * Mathf.PI * 2f / seg;
            Vector3 next = center + new Vector3(Mathf.Cos(a) * r, Mathf.Sin(a) * r, 0);
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
}
