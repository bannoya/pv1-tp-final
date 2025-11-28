using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet;
    public Camera playerCamera;

    public float shootCooldown = 1f; 
    private float lastShootTime = 0f;

    AudioManagerLevel1 audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLevel1>();
    }

    public void Shoot()
    {
        // ⛔ Respetar cooldown
        if (Time.time < lastShootTime + shootCooldown)
            return;

        lastShootTime = Time.time;

        // Convertir mouse a mundo
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = playerCamera.nearClipPlane;
        Vector3 mouseWorldPos = playerCamera.ScreenToWorldPoint(mousePos);

        // Dirección
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Spawn
        Vector3 spawnPos = transform.position + (Vector3)(direction * 0.5f);
        GameObject bullet = Instantiate(playerBullet, spawnPos, Quaternion.identity);

        // Ignorar colisión con player
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (playerCollider != null && bulletCollider != null)
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);


        // Inicializar bala
        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        if (bulletScript != null)
            bulletScript.Init(direction);

        audioManager.PlaySFX(audioManager.shoot);
    }
}
