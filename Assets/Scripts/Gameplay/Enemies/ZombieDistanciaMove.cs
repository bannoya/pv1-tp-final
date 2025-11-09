using UnityEngine;

public class ZombieDistancia : MonoBehaviour
{
    public string playerTag = "Player";   // Tag del jugador
    public float velocidad = 2f;
    public float rangoAtaque = 4f;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.5f;

    private Transform player;
    private float contadorDisparo;
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
            spriteRenderer.flipX = true; // mira a la derecha
        }
        else
        {
            spriteRenderer.flipX = false; // mira a la izquierda
        }

        // Si está fuera del rango de ataque, moverse hacia el jugador
        if (distancia > rangoAtaque)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
            animator.SetBool("isAtacking", false);
        }
        else
        {
            // Si está dentro del rango, quedarse quieto y disparar
            contadorDisparo -= Time.deltaTime;

            if (contadorDisparo <= 0f)
            {
                Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
                contadorDisparo = tiempoEntreDisparos;
            }

            animator.SetBool("isAtacking", true);
        }
    }
}
