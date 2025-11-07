using UnityEngine;
public class SortByYAnchor : MonoBehaviour
{
    public Transform anchor;
    public int offset = 0;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        float y = (anchor ? anchor.position.y : transform.position.y);
        sr.sortingOrder = Mathf.RoundToInt(-y * 100) + offset;
    }
}
