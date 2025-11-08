using UnityEngine;

public class ClickChop : MonoBehaviour
{
    public Camera mainCam;
    public float chopDistance = 5f;
    public LayerMask chopMask; // Layer chứa cây/gốc
    public Animator playerAnimator;
    public string chopTrigger = "TreeAxe";
    public string moveXParam = "MoveX";
    public string moveYParam = "MoveY";
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
        if (playerAnimator && !string.IsNullOrEmpty(chopTrigger))
        {
            playerAnimator.ResetTrigger(chopTrigger);
            playerAnimator.SetTrigger(chopTrigger);
        }
        Debug.Log("[ClickChop] Chặt: " + tree.name);

        // 1) Tính hướng từ player -> cây
         Vector2 dir = (tree.transform.position - transform.position);

         // 2) Quy về 4 hướng
         if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
          {
         // ngang
         dir.x = dir.x > 0 ? 1f : -1f;
        dir.y = 0f;
         }
         else
        {
         // dọc
        dir.y = dir.y > 0 ? 1f : -1f;
        dir.x = 0f;
        }

        // 3) Set hướng cho Blend Tree TreeAxe
        if (playerAnimator)
        {
        if (!string.IsNullOrEmpty(moveXParam))
        playerAnimator.SetFloat(moveXParam, dir.x);

        if (!string.IsNullOrEmpty(moveYParam))
        playerAnimator.SetFloat(moveYParam, dir.y);

        // (nếu Idle/Walk dùng LastX/LastY thì set thêm ở đây tương tự)
     // playerAnimator.SetFloat("LastX", dir.x);
        // playerAnimator.SetFloat("LastY", dir.y);

        // 4) Trigger anim chặt
        if (!string.IsNullOrEmpty(chopTrigger))
        {
        playerAnimator.ResetTrigger(chopTrigger);
        playerAnimator.SetTrigger(chopTrigger);
     }
        }

    // 5) Gọi chặt cây
        tree.Hit();
    }
}
