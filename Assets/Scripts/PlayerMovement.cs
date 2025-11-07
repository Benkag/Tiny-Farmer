using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Character character; // Tham chiếu đến script Character

    void Awake()
    {
        character = GetComponent<Character>();

        // Nếu Character không có component cần thiết thì tự động gán trong script Character
        if (character.rb == null) character.rb = GetComponent<Rigidbody2D>();
        if (character.spriteRenderer == null) character.spriteRenderer = GetComponent<SpriteRenderer>();
        if (character.animator == null) character.animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Di chuyển nhân vật bằng Rigidbody2D
        character.rb.linearVelocity = moveInput * speed;

        // Gửi dữ liệu sang Character để xử lý animation
        character.SetMove(moveInput);
    }
}
