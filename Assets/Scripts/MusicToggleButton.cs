using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private Image buttonImage;
    private bool isMusicOn = true;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // Load trạng thái đã lưu
        isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;

        UpdateMusicState();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);

        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        // Cập nhật hình nút
        buttonImage.sprite = isMusicOn ? musicOnSprite : musicOffSprite;

        // Bật/tắt nhạc nền qua AudioManager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic(isMusicOn);
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null! Please add AudioManager to the Scene.");
        }
    }
}
