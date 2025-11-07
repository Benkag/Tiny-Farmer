using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class TreeFullChop : MonoBehaviour
{
    public int maxHp = 3;
    public GameObject logPrefab;        // Prefab gỗ
    public int logAmount = 2;           // Gỗ rơi khi chặt cây full
    public float dropRadius = 0.4f;     // Bán kính rơi

    public GameObject stumpPrefab;      // Prefab Tree_Stump
    public Transform stumpPoint;        // Điểm spawn stump (anchor ở chân cây)

    int hp;

    void Awake()
    {
        hp = maxHp;
    }

    public void Hit()
    {
        if (hp <= 0) return;

        hp--;
        Debug.Log($"[TreeFullChop] Hit {name}, hp = {hp}");

        if (hp <= 0)
        {
            ChopDown();
        }
    }

    void ChopDown()
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

        // Spawn stump đúng vị trí gốc
        if (stumpPrefab != null)
        {
            Vector3 spawnPos = stumpPoint != null ? stumpPoint.position : transform.position;
            Instantiate(stumpPrefab, spawnPos, Quaternion.identity);
        }

        // Xoá cây full
        Destroy(gameObject);
    }
}
