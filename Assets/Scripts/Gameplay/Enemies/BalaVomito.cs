using UnityEngine;

public class BalaVomito : MonoBehaviour
{
    public string playerTag = "Player";   // Tag del jugador
    public float velocidad = 10f;
    public float tiempoVida = 3f;
    public int damage = 10;               // <<< Daño de la bala

    private Transform objetivo;
    private Vector2 direccion;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObj != null)
        {
            objetivo = playerObj.transform;
            direccion = (objetivo.position - transform.position).normalized;
        }

        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // Aplicar daño
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            // Destruir bala
            Destroy(gameObject);
        }
    }
}
