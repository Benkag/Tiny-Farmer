using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private Vector2 currentVelocity;
    private Vector2 lastMoveDirection;  // Hướng di chuyển cuối

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void SetMove(Vector2 moveInput)
    {
        currentVelocity = moveInput;

        if (moveInput != Vector2.zero)
            lastMoveDirection = moveInput;

        // Xử lý flip sprite trái/phải
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("MoveX", currentVelocity.x);
        animator.SetFloat("MoveY", currentVelocity.y);
        animator.SetFloat("LastX", lastMoveDirection.x);
        animator.SetFloat("LastY", lastMoveDirection.y);
        animator.SetFloat("Speed", currentVelocity.sqrMagnitude);
    }
}
