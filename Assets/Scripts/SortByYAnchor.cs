using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortByYAnchor : MonoBehaviour
{
    [Header("Sorting theo vị trí Y (anchor là chân đối tượng)")]
    public Transform anchor;       // điểm chân dùng để tính
    public int offset = 0;         // chỉnh nhỏ nếu cần ưu tiên
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (!sr)
            Debug.LogError($"[SortByYAnchor] {name} không có SpriteRenderer!");
    }

    void LateUpdate()
    {
        if (!sr) return;

        float y = (anchor ? anchor.position.y : transform.position.y);
        sr.sortingOrder = Mathf.RoundToInt(-y * 100) + offset;
    }
}
