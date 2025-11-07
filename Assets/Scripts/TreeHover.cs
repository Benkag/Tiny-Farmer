using UnityEngine;

public class TreeHover : MonoBehaviour
{
    SpriteRenderer sr;
    Color baseColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    void OnMouseEnter()
    {
        sr.color = Color.Lerp(baseColor, Color.yellow, 0.3f);
    }

    void OnMouseExit()
    {
        sr.color = baseColor;
    }
}
