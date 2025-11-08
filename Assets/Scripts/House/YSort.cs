using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    public int sortingOffset = 0;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // càng thấp thì order càng cao (vẽ trên)
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + sortingOffset;
    }
}
