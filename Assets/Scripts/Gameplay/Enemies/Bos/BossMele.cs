using System.Collections;
using UnityEngine;

public class BossMele : MonoBehaviour
{
    [Header("Target & movement")]
    public string playerTag = "Player";
    public float velocidad = 2.5f; // velocidad normal al caminar

    [Header("Ranges")]
    public float meleeDistance = 0f;
    public float meleeThreshold = 0.05f;
    public float rangedRadius = 5f;

    [Header("Melee / Charge (envestida)")]
    public int meleeDamage = 15;
    public float chargeInterval = 4f;          // cada cuánto puede iniciar la envestida
    public float chargeActivationDistance = 6f; // sólo inicia la carga si el player está dentro de este radio
    public float chargeSpeed = 8f;             // velocidad durante la envestida
    public float chargeDuration = 1.0f;        // tiempo máximo que dura la envestida
    public float chargeStopDistance = 0.2f;    // distancia para considerar "impacto" durante la carga
    public float meleeAnimDuration = 0.6f;
    public float meleeHitTime = 0.25f;

    [Header("Ranged (vomito)")]
    public GameObject vomitoPrefab;
    public Transform firePoint;
    public int rangedDamage = 10;
    public float rangedSpeed = 10f;
    public float rangedCooldown = 1.5f;

    [Header("Components")]
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    // estado
    private Transform player;
    private PlayerHealth playerHealth;
    private bool isAttacking = false;
    private bool isCharging = false;
    private float rangedTimer = 0f;
    private float chargeTimer = 0f; // control interno para iniciar carga

    [System.Obsolete]
    void Start()
    {
        Collider2D col1 = GetComponent<Collider2D>();
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D col in allColliders)
        {
            if (col != col1 && (col.CompareTag("ZombieRange") || col.CompareTag("ZombieMele")||col.CompareTag("Player")))
            {
                Physics2D.IgnoreCollision(col1, col);
            }
        }

        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
        if (player != null) playerHealth = player.GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();

        chargeTimer = chargeInterval; // empezar contando hasta la primera carga
    }

    void Update()
    {
        if (player == null) return;

        // timers
        chargeTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;

        // flip
        if (spriteRenderer != null)
            spriteRenderer.flipX = player.position.x > transform.position.x;

        // bloqueo si estamos en ataque
        if (isAttacking) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        // Iniciar la envestida sólo si:
        // 1) pasó el intervalo chargeInterval (chargeTimer <= 0)
        // 2) el player está dentro de chargeActivationDistance
        if (chargeTimer <= 0f && !isCharging && distancia <= chargeActivationDistance)
        {
            StartCoroutine(PerformCharge());
            return;
        }

        // Si player está dentro de meleeThreshold (cercano) y no estamos cargando, quedarnos quietos
        if (distancia <= meleeDistance + meleeThreshold)
        {
            if (animator != null) animator.SetBool("isMele", false);
            return;
        }

        // Si estamos dentro del radio de rango -> ataque a distancia
        if (distancia <= rangedRadius)
        {
            if (rangedTimer <= 0f)
            {
                StartCoroutine(PerformRanged());
            }
            return;
        }

        // Si no estamos en ranges, movernos normalmente hacia el player
        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        Vector2 direccion = (player.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        if (animator != null)
        {
            animator.SetBool("isMele", false);
            animator.SetBool("isDistance", false);
      
        }
    }

    private IEnumerator PerformCharge()
    {
        // Preparación de la carga
        isAttacking = true;
        isCharging = true;
        chargeTimer = chargeInterval; // reiniciar timer para la próxima carga

        if (animator != null) animator.SetBool("isMele", true);

        float timer = 0f;

        // perseguir la posición actual del player durante la carga
        while (timer < chargeDuration)
        {
            timer += Time.deltaTime;

            Vector3 dirNow = (player.position - transform.position).normalized;
            transform.position += dirNow * chargeSpeed * Time.deltaTime;

            // comprobar si impactamos (distancia pequeña)
            float curDist = Vector2.Distance(transform.position, player.position);
            if (curDist <= chargeStopDistance)
            {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(meleeDamage);
                }
                break; // terminamos la carga al impactar
            }

            yield return null;
        }

        // pequeña espera para que la animación termine correctamente (ajustable)
        float rest = Mathf.Max(0f, meleeAnimDuration - meleeHitTime);
        yield return new WaitForSeconds(rest);

        if (animator != null) animator.SetBool("isMele", false);

        isCharging = false;
        isAttacking = false;
    }

    // respaldo por colisiones físicas durante la carga
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCharging) return;

        if (other.CompareTag(playerTag))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(meleeDamage);
            }
            // finalizar carga
            isCharging = false;
            isAttacking = false;
            if (animator != null) animator.SetBool("isMele", false);
        }
    }

    private IEnumerator PerformRanged()
    {
        isAttacking = true;
        rangedTimer = rangedCooldown;

        if (animator != null) animator.SetBool("isDistance", true);

        float preShootDelay = 0.12f;
        yield return new WaitForSeconds(preShootDelay);

        if (vomitoPrefab != null)
        {
            Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;
            GameObject v = Instantiate(vomitoPrefab, spawnPos, Quaternion.identity);
            BalaVomito bv = v.GetComponent<BalaVomito>();
            if (bv != null)
            {
                bv.damage = rangedDamage;
                bv.velocidad = rangedSpeed;
            }
            else
            {
                Rigidbody2D rb = v.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = (player.position - spawnPos).normalized;
                    rb.linearVelocity = dir * rangedSpeed;
                }
            }
        }

        float postShootDelay = 0.2f;
        yield return new WaitForSeconds(postShootDelay);

        if (animator != null) animator.SetBool("isDistance", false);
        isAttacking = false;
    }
}
