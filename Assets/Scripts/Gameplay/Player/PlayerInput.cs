using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShoot))]
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
            // ⛔ Bloquear clics sobre la UI (como la barra de vida)
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            playerShoot.Shoot();
        }
    }
}
