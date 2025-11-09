using UnityEngine;

public class ZombieMele : MonoBehaviour
{
    public string playerTag = "Player";   // Tag del jugador
    public float velocidad = 2f;
    public float rangoAtaque = 1.5f;
    public float tiempoEntreAtaques = 1.2f;

    private Transform player;
    private float contadorAtaque;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!player) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        // 🔁 Flip automático según la posición del jugador
        if (player.position.x > transform.position.x)
        {
            // jugador a la derecha → mirar a la derecha
            spriteRenderer.flipX = true;
        }
        else
        {
            // jugador a la izquierda → mirar a la izquierda
            spriteRenderer.flipX = false;
        }

        // 🚶‍♂️ Si está fuera del rango de ataque, moverse hacia el jugador
        if (distancia > rangoAtaque)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        else
        {
            // 🧟‍♂️ Si está dentro del rango, quedarse quieto y atacar
            animator.SetBool("isWalking", false);
            contadorAtaque -= Time.deltaTime;

            if (contadorAtaque <= 0f)
            {
              
                contadorAtaque = tiempoEntreAtaques;
            }
        }
    }
}
