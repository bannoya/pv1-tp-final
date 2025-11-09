using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShoot))] // 🔥 Asegura que haya un PlayerShoot en el mismo objeto
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private Vector2 input;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    void Update()
    {
        // Movimiento clásico
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        playerMovement.SetInput(input);

        // Disparo con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            playerShoot.Shoot();
        }
    }
}
