using UnityEngine;

public class ClickChop : MonoBehaviour
{
    public Camera mainCam;
    public float chopDistance = 2f;
    public LayerMask chopMask; // Layer cho cây & stump

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryChop();
        }
    }

    void TryChop()
    {
        if (!mainCam)
        {
            Debug.LogWarning("[ClickChop] MainCam chưa gán");
            return;
        }

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(mouseWorld.x, mouseWorld.y);

        Collider2D hit = Physics2D.OverlapCircle(point, 0.15f, chopMask);

        if (!hit)
        {
            Debug.Log("[ClickChop] Không trúng gì");
            return;
        }

        // Ưu tiên tìm trên object cha (trường hợp collider nằm trên child)
        var full = hit.GetComponentInParent<TreeFullChop>();
        if (full != null)
        {
            if (Vector2.Distance(transform.position, full.transform.position) <= chopDistance)
                full.Hit();
            return;
        }

        var stump = hit.GetComponentInParent<TreeStumpChop>();
        if (stump != null)
        {
            if (Vector2.Distance(transform.position, stump.transform.position) <= chopDistance)
                stump.Hit();
            return;
        }

        Debug.Log("[ClickChop] Trúng " + hit.name + " nhưng không có script chặt.");
    }
}
