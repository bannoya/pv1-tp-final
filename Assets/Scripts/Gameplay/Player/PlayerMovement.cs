using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // Este método lo llama PlayerInput
    public void SetInput(Vector2 input)
    {
        movementInput = input;

        // --- Limpieza de booleans ---
        bool isUp = false;
        bool isBot = false;
        bool isHorizontal = false;

        // --- Detectar dirección ---
        if (input.y > 0.1f)
        {
            isUp = true;
        }
        else if (input.y < -0.1f)
        {
            isBot = true;
        }
        else if (Mathf.Abs(input.x) > 0.1f)
        {
            isHorizontal = true;
        }

        // --- Animación ---
        animator.SetBool("isUp", isUp);
        animator.SetBool("isBot", isBot);
        animator.SetBool("isHorizontal", isHorizontal);

        // Si no hay movimiento → Idle
        if (input == Vector2.zero)
        {
            animator.SetBool("isUp", false);
            animator.SetBool("isBot", false);
            animator.SetBool("isHorizontal", false);
        }

        // --- Rotación izquierda / derecha ---
        if (input.x > 0.1f)
        {
            transform.localScale = new Vector3(-4, 4, 4);     // mirando derecha
        }
        else if (input.x < -0.1f)
        {
            transform.localScale = new Vector3(4, 4, 4);    // mirando izquierda
        }
    }

    void FixedUpdate()
    {
        // Movimiento físico
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}
