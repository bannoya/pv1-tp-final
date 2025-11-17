using UnityEngine;

public class ZombieMele : MonoBehaviour
{
    public string playerTag = "Player"; // Tag del jugador
    public float velocidad = 2f;
    public float rangoAtaque = 1.5f;
    public float tiempoEntreAtaques = 1.2f;
    public int danoAtaque = 10;
    private float contadorAtaque;
    
    private Transform player;
    private PlayerHealth playerHealth;


    [SerializeField]
    private Animator animator;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (!player) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        // 🔁 Flip automático según la posición del jugador
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;   // Mirar derecha
        }
        else
        {
            spriteRenderer.flipX = false;  // Mirar izquierda
        }

        // 🚶‍♂️ Moverse si está fuera del rango de ataque
        if (distancia > rangoAtaque)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;

            animator.SetBool("isWalking", true);
        }
        else
        {
            // 🧟‍♂️ Dentro del rango → detenerse y atacar
            animator.SetBool("isWalking", false);

            contadorAtaque -= Time.deltaTime;
            if (contadorAtaque <= 0f)
            {
                contadorAtaque = tiempoEntreAtaques;
                playerHealth?.TakeDamage(danoAtaque);
            }
        }
    }
}
