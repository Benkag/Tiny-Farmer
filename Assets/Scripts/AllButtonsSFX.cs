using UnityEngine;
using UnityEngine.UI;

public class AllButtonsSFX : MonoBehaviour
{
    public AudioClip clickSFX;

    void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach(Button btn in buttons)
        {
            btn.onClick.AddListener(() =>
            {
                if(AudioManager.Instance != null)
                    AudioManager.Instance.PlaySFX(clickSFX);
            });
        }
    }
}
