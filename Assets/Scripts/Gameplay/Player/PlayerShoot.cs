using UnityEngine;

public class PlayerShoot2D : MonoBehaviour
{
    public GameObject playerBullet;
    public float bulletSpeed = 10f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Calcular dirección
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Instanciar la bala
        GameObject bullet = Instantiate(playerBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        Destroy(bullet, 1f);
    }
}
