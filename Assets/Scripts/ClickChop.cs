using UnityEngine;

public class ClickChop : MonoBehaviour
{
    public Camera mainCam;
    public float chopDistance = 2f;
    public LayerMask chopMask; // Layer chứa cây/gốc

    void Update()
    {
    if (Input.GetMouseButtonDown(0))   // phải là GetMouseButtonDown
    {
        TryChop();
    }
    }   


    void TryChop()
    {
        if (!mainCam)
        {
            Debug.LogWarning("[ClickChop] Main Camera chưa gán");
            return;
        }

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = new Vector2(mouseWorld.x, mouseWorld.y);

        // vùng click nhỏ quanh chuột
        Collider2D hit = Physics2D.OverlapCircle(point, 0.15f, chopMask);

        if (!hit)
        {
            Debug.Log("[ClickChop] Không trúng gì");
            return;
        }

        TreeChop tree = hit.GetComponentInParent<TreeChop>();
        if (!tree)
        {
            Debug.Log("[ClickChop] Không có TreeChop trên " + hit.name);
            return;
        }

        // check khoảng cách player - cây/gốc
        float dist = Vector2.Distance(transform.position, tree.transform.position);
        if (dist > chopDistance)
        {
            Debug.Log("[ClickChop] Quá xa để chặt");
            return;
        }

        Debug.Log("[ClickChop] Chặt: " + tree.name);
        tree.Hit();
    }
}
