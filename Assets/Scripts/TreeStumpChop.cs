using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class TreeStumpChop : MonoBehaviour
{
    public int maxHp = 2;
    public GameObject logPrefab;       // Prefab gỗ
    public int logAmount = 1;          // Gỗ rơi khi chặt stump
    public float dropRadius = 0.3f;

    int hp;

    void Awake()
    {
        hp = maxHp;
    }

    public void Hit()
    {
        if (hp <= 0) return;

        hp--;
        Debug.Log($"[TreeStumpChop] Hit {name}, hp = {hp}");

        if (hp <= 0)
        {
            BreakStump();
        }
    }

    void BreakStump()
    {
        // Spawn gỗ
        if (logPrefab != null && logAmount > 0)
        {
            for (int i = 0; i < logAmount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * dropRadius;
                Vector2 pos = (Vector2)transform.position + offset;
                Instantiate(logPrefab, pos, Quaternion.identity);
            }
        }

        // Xoá stump
        Destroy(gameObject);
    }
}
