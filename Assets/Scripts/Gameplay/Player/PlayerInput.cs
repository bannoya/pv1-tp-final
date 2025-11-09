using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Vector de entrada pública de solo lectura (usada por PlayerMovement)
    public Vector2 MoveInput { get; private set; }

    // Para marcar este objeto como jugador
    [SerializeField] private bool isPlayer = true;

    void Update()
    {
        // Si este script no pertenece al jugador, no hace nada
        if (!isPlayer)
        {
            MoveInput = Vector2.zero;
            return;
        }

        // Leer entrada solo si es el jugador
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(moveX, moveY).normalized;
    }
}
