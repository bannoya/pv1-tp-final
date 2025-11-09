using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet;
    public Camera playerCamera;

    public void Shoot()
    {
        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 mouseWorldPos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Calcular dirección normalizada hacia el mouse
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Posición de spawn un poco delante del jugador
        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.5f);

        // Instanciar la bala
        GameObject bullet = Instantiate(playerBullet, spawnPos, Quaternion.identity);

        // Ignorar colisión con el jugador
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (playerCollider != null && bulletCollider != null)
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);

        // Inicializar la bala con la dirección
        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        if (bulletScript != null)
            bulletScript.Init(direction);
    }
}
