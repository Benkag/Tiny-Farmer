using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Buttons")]
    public Button btnPlay;
    public Button btnContinue;
    public Button btnOptions;
    public Button btnExit;
    public Button btnCredits;

    [Header("Social Buttons")]
    public Button btnFacebook;
    public Button btnDiscord;

    [Header("Panels")]
    public GameObject panelCredits;
    public Button btnCloseCredits;

    public GameObject panelOptions;
    public Button btnCloseOptions;

    [Header("Options Toggles")]
    public Toggle toggleMusic;
    public Toggle toggleSound;

    [Header("Options Sliders")]
    public Slider sliderMusicVolume;
    [Header("Options Sliders")]
    public Slider sliderFXVolume; // Slider điều chỉnh FX


    private void Start()
    {
        // Main buttons
        AddButtonListener(btnPlay, OnPlay);
        AddButtonListener(btnContinue, OnContinue);
        AddButtonListener(btnExit, OnExit);
        AddButtonListener(btnCredits, () => SetPanelActive(panelCredits, true));
        AddButtonListener(btnOptions, () => SetPanelActive(panelOptions, true));

        // Panel close buttons
        AddButtonListener(btnCloseCredits, () => SetPanelActive(panelCredits, false));
        AddButtonListener(btnCloseOptions, () => SetPanelActive(panelOptions, false));

        // Social buttons
        AddButtonListener(btnFacebook, () => OpenURL("https://web.facebook.com/NghiaPhung.116.204"));
        AddButtonListener(btnDiscord, () => OpenURL("https://discord.com/invite/9kSNXdDGuv"));

        // Panel initial state
        panelCredits.SetActive(false);
        panelOptions.SetActive(false);
        btnPlay.onClick.AddListener(() => SceneManager.LoadScene("CharacterSelection"));
        btnContinue.onClick.AddListener(() => SceneManager.LoadScene("#"));
        // Option toggles
        if(toggleMusic != null)
        {
            toggleMusic.isOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
            toggleMusic.onValueChanged.AddListener(OnMusicToggle);
        }

        if (toggleSound != null)
        {
            toggleSound.isOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
            toggleSound.onValueChanged.AddListener(OnSoundToggle);
        }
        if(sliderMusicVolume != null)
        if(sliderFXVolume != null)
{
         sliderFXVolume.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

         sliderFXVolume.onValueChanged.AddListener((value) =>
        {
        PlayerPrefs.SetFloat("SFXVolume", value);
        if(AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
         });

         if(AudioManager.Instance != null)
        AudioManager.Instance.SetSFXVolume(sliderFXVolume.value);
}
    {
    // Load giá trị volume đã lưu trước đó
    sliderMusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

    // Khi slider thay đổi
    sliderMusicVolume.onValueChanged.AddListener((value) =>
    {
        PlayerPrefs.SetFloat("MusicVolume", value); // Lưu lại
        if(AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value); // Thay đổi realtime
    });

    // Áp dụng luôn lúc Start
    if(AudioManager.Instance != null)
        AudioManager.Instance.SetMusicVolume(sliderMusicVolume.value);
    }

    }

    #region Button Helpers
    private void AddButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if(button != null)
            button.onClick.AddListener(action);
    }

    private void SetPanelActive(GameObject panel, bool state)
    {
        if(panel != null)
            panel.SetActive(state);
    }

    private void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
    #endregion

    #region Main Button Actions
    private void OnPlay() => SceneManager.LoadScene("CharacterSelection");
    private void OnContinue() => SceneManager.LoadScene("#");
    private void OnExit() => Application.Quit();
    #endregion

    #region Options Toggles
    private void OnMusicToggle(bool isOn)
    {
        PlayerPrefs.SetInt("MusicState", isOn ? 1 : 0);
        Debug.Log("Music " + (isOn ? "On" : "Off"));
        // TODO: Thêm logic bật/tắt nhạc nền
    }

    private void OnSoundToggle(bool isOn)
    {
        PlayerPrefs.SetInt("SoundState", isOn ? 1 : 0);
        Debug.Log("Sound " + (isOn ? "On" : "Off"));
        // TODO: Thêm logic bật/tắt âm thanh
    }
    #endregion

}