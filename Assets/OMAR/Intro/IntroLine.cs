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

    [Header("Image Effects")]
    public bool shakeEffect = false;
    public float shakeIntensity = 10f;
    public float shakeSpeed = 20f;

    public bool zoomEffect = false;
    public float zoomAmount = 1.05f;
    public float zoomSpeed = 0.5f;

    public bool panEffect = false;
    public float panAmount = 20f;
    public float panSpeed = 0.5f;

    [Header("Audio")]
    public AudioClip sound;
}
