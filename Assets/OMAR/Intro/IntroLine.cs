using UnityEngine;
using TMPro;

[System.Serializable]
public class IntroLine
{
    [Header("Text")]
    [TextArea(5, 20)] public string text;
    public TMP_FontAsset font;
    public float textDisplayTime = 3f;
    public Color textColor = Color.white;

    [Header("Image")]
    public Sprite image;
    public float imageFadeInTime = 1f;
    public float imageFadeOutTime = 1f;
    public float imageDisplayTime = 3f;

    [Header("Audio")]
    public AudioClip sound;
}
