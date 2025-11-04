using UnityEngine;

public class Enemie_Bullet: MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;
    public int damage = 1;

    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
        // opcional: rotar la bala hacia la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: acá aplicá daño al jugador (ej: other.GetComponent<PlayerHP>()?.TakeDamage(damage);)
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            // cualquier colisión sólida destruye la bala
            Destroy(gameObject);
        }
    }
}
