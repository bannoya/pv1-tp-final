using UnityEngine;

public class BalaVomito : MonoBehaviour
{
    public string playerTag = "Player";   // Tag del jugador
    public float velocidad = 10f;
    public float tiempoVida = 3f;

    private Transform objetivo;
    private Vector2 direccion;

    void Start()
    {
        // Buscar al jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            objetivo = playerObj.transform;
            // Dirección hacia el jugador en el momento del disparo
            direccion = (objetivo.position - transform.position).normalized;
        }

        // Destruir la bala automáticamente después de cierto tiempo
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        // Mover la bala en la dirección calculada
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si toca al jugador o algo sólido, se destruye
        if (other.CompareTag(playerTag) )
        {
            Destroy(gameObject);
        }
    }
}
