using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button nextButton;

    [Header("Speaker Image")]
    public Image speakerImage;

    [Header("Audio")]
    public AudioSource audioSource;

    private DialogueData currentData;
    private int index;

    private NPCDialogue npcRef;

    //typing effect
    private bool isTyping = false;
    private string fullText;
    private float currentTypingSpeed = 0.03f;

    public bool IsOpen => panel.activeSelf;

    void Start()
    {
        panel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
    }

    void Update()
    {
        if (!panel.activeSelf) return;

        //f control the dialogue
        if (npcRef != null && npcRef.ConsumeJustOpenedFlag())
            return;

        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
            NextLine();
    }

    public void StartDialogue(DialogueData data, NPCDialogue npc)
    {
        npcRef = npc;
        currentData = data;
        index = 0;

        panel.SetActive(true);
        ShowLine();
    }

    public void ForceCloseDialogue()
    {
        EndDialogue();
    }

    void ShowLine()
    {
        if (currentData == null || index >= currentData.lines.Length)
        {
            EndDialogue();
            return;
        }

        var line = currentData.lines[index];

        nameText.text = line.speakerName;

        //sprite on the lane
        if (speakerImage != null)
        {
            if (line.speakerSprite != null)
            {
                speakerImage.sprite = line.speakerSprite;
                speakerImage.color = Color.white;
            }
            else
            {
                speakerImage.color = Color.clear;
            }
        }

        //sound of the lane
        if (audioSource != null)
        {
            audioSource.Stop();
            if (line.voiceClip != null)
                audioSource.PlayOneShot(line.voiceClip);
        }

        //speed on lane
        currentTypingSpeed = line.typingSpeed;

        //typing effect on lane
        StopAllCoroutines();
        StartCoroutine(TypeText(line.text));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        fullText = text;

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(currentTypingSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = fullText;
            isTyping = false;
            return;
        }

        index++;
        ShowLine();
    }

    void EndDialogue()
    {
        panel.SetActive(false);
        currentData = null;

        if (npcRef != null)
            npcRef.RestoreMovement();

        npcRef = null;
    }
}
