using UnityEngine;
using UnityEngine.UI;

public class FXVolumeSlider : MonoBehaviour
{
    public Slider sliderSFX;

    void Start()
    {
        if(sliderSFX != null)
        {
            // Lấy volume đã lưu
            sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

            sliderSFX.onValueChanged.AddListener((value) =>
            {
                PlayerPrefs.SetFloat("SFXVolume", value);
                if(AudioManager.Instance != null)
                    AudioManager.Instance.SetSFXVolume(value);
            });

            // Áp dụng ngay khi Start
            AudioManager.Instance.SetSFXVolume(sliderSFX.value);
        }
    }
}
