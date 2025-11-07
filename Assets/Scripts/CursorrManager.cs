using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;    // Con trỏ mặc định
    [SerializeField] private Texture2D cursorClicked;   // Con trỏ khi click
    private Vector2 hotSpot = new Vector2(16, 48); // Điểm nóng

    private void Start()
    {
        Cursor.SetCursor(cursorNormal, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClicked, hotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hotSpot, CursorMode.Auto);
        }
    }
}
