using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TreeChop : MonoBehaviour
{
    [Header("HP")]
    public int maxHp = 3;

    [Header("Drop Wood")]
    public GameObject logPrefab;      // Gán prefab gỗ vào đây
    public int logAmount = 2;         // Số gỗ rơi
    public float dropRadius = 0.8f;   // Bán kính rơi quanh cây

    [Header("Animation")]
    public Animator animator;          // Gán Animator cây
    public string hitTrigger = "Hit";  // Trigger trong Animator

    int hp;

    void Awake()
    {
        hp = maxHp;
        if (!animator)
            animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        if (hp <= 0) return;

        hp--;

        Debug.Log($"[TreeChop] Hit {name}, hp = {hp}");

        // mỗi hit bắn trigger 1 lần
        if (animator && !string.IsNullOrEmpty(hitTrigger))
        {
            animator.ResetTrigger(hitTrigger);
            animator.SetTrigger(hitTrigger);
        }

        // khi hết máu -> phá cây
        if (hp <= 0)
        {
            BreakSelf();
        }
    }

    void BreakSelf()
    {
        Debug.Log($"[TreeChop] BREAK {name}: spawn {logAmount} wood");

        if (logPrefab == null)
        {
            Debug.LogError($"[TreeChop] {name}: logPrefab CHƯA GÁN");
        }
        else if (logAmount <= 0)
        {
            Debug.LogWarning($"[TreeChop] {name}: logAmount = {logAmount}, nên không rơi gỗ");
        }
        else
        {
            for (int i = 0; i < logAmount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * dropRadius;
                Vector2 pos = (Vector2)transform.position + offset;
                Instantiate(logPrefab, pos, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
