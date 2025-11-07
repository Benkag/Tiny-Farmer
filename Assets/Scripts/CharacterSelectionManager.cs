using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    public Button[] characterButtons;
    public Button confirmButton;
    public Button backButton;
    public Image selectedCharacterPortrait;
    public Text selectedNameText;
    public ScrollRect scrollRect;

    [Header("Player Prefabs")]
    public GameObject[] playerPrefabs;

    private int selectedIndex = -1;
    private string selectedName;

    void Start()
    {
        if (characterButtons == null || characterButtons.Length == 0)
        {
        Debug.LogError("[CharacterSelection] characterButtons chưa được gán!");
        return;
        }

        // GÁN NÚT NHÂN VẬT
        for (int i = 0; i < characterButtons.Length; i++)
        {
            if (characterButtons[i] == null) continue;
            int index = i;
            characterButtons[i].onClick.RemoveAllListeners();
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }

        // GÁN NÚT CONFIRM
        if (confirmButton != null)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(ConfirmSelection);
            confirmButton.interactable = false;
        }
        else Debug.LogError("confirmButton chưa được gán!");

        // GÁN NÚT BACK
        if (backButton != null)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(BackToMainMenu);
            Debug.Log("NÚT BACK ĐÃ ĐƯỢC GÁN VÀ HOẠT ĐỘNG!");
        }
        else Debug.LogError("BACK BUTTON CHƯA ĐƯỢC GÁN TRONG INSPECTOR!");
    }

    // ĐƯA RA NGOÀI Start() – QUAN TRỌNG!
    void SelectCharacter(int index)
    {
        selectedIndex = index;
        selectedName = characterButtons[index].name.Replace("Button", "").Trim();

        // Highlight
        for (int i = 0; i < characterButtons.Length; i++)
        {
            if (characterButtons[i] == null) continue;
            characterButtons[i].transform.localScale = Vector3.one * (i == index ? 1.15f : 1f);
        }

        // Preview
        if (selectedCharacterPortrait != null)
            selectedCharacterPortrait.sprite = characterButtons[index].GetComponent<Image>().sprite;

        if (selectedNameText != null)
            selectedNameText.text = selectedName.ToUpper();

        // Bật Confirm
        if (confirmButton != null)
            confirmButton.interactable = true;

        // Cuộn tự động
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            float pos = (float)index / (characterButtons.Length - 1);
            scrollRect.verticalNormalizedPosition = 1f - pos;
        }
    }

    void ConfirmSelection()
    {
        if (selectedIndex >= 0 && !string.IsNullOrEmpty(selectedName))
        {
            PlayerPrefs.SetString("SelectedPlayer", selectedName);
            PlayerPrefs.Save();
            Debug.Log("Đã chọn nhân vật: " + selectedName);
            SceneManager.LoadScene("GameScene");
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (scrollRect != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                scrollRect.verticalNormalizedPosition += scroll * 50f;
                scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMainMenu();
    }
}