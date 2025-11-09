using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerInput))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerInput input;

    void Awake()
    {
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 move = input.MoveInput;
        bool isMoving = move.sqrMagnitude > 0;

        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("MoveX", move.x);
        anim.SetFloat("MoveY", move.y);
    }
}
