using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // Panel Menu từ Inspector

    private bool isMenuActive = false;

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);

        // Tùy chọn: dừng game khi mở menu
        Time.timeScale = isMenuActive ? 0f : 1f;
    }
}
