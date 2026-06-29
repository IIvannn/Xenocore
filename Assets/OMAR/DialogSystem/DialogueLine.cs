using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea] public string text;

    public AudioClip voiceClip;

    [Header("Typing Speed")]
    public float typingSpeed = 0.03f;

    [Header("Options")]
    public bool showOptionsHere = false;

    [Header("Return to previous dialogue")]
    public bool jumpBackToPreviousDialogue = false;
    public int previousDialogueLineIndex = 0;

    [Header("Portraits")]
    public Sprite speakerSprite;   //sprite of the right
    public bool isLeftSpeaker;     //left speaking on or off
    public Sprite leftPortrait;    //sprite of the left

    [Header("Font")]
    public TMP_FontAsset customFont; //font used
}
