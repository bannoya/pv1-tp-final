using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private readonly float moveSpeed = 5f;

    private UnityEngine.Rigidbody2D rb;
    private PlayerInput input;

    void Awake()
    {
        rb = GetComponent<UnityEngine.Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input.MoveInput * moveSpeed;

    }
}
