using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea] public string text;

    public AudioClip voiceClip;//sound
    public Sprite speakerSprite;//sprite

    [Header("Typing Speed")]
    public float typingSpeed = 0.03f; //speed of the lane
}
