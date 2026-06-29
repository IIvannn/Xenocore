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
    public Image leftPortrait;


    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Options UI")]
    public GameObject optionsPanel;
    public Button optionButtonPrefab;

    private DialogueData currentData;
    private int index;

    private NPCDialogue npcRef;
    private bool optionsActive = false;

    // typing effect
    private bool isTyping = false;
    private string fullText;
    private float currentTypingSpeed = 0.03f;

    //playeshoot stop
    public bool IsOpen => panel.activeSelf;
    public bool OptionsActive => optionsActive;

    //revious dialogue in options
    private DialogueData previousData;
    private bool hasPreviousDialogue = false;

    void Start()
    {
        panel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (!panel.activeSelf) return;

        if (npcRef != null && npcRef.ConsumeJustOpenedFlag())
            return;

        if (!optionsActive)
        {
            if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
                NextLine();
        }
    }

    public void StartDialogue(DialogueData data, NPCDialogue npc, int startIndex = 0)
    {
        npcRef = npc;
        currentData = data;
        index = Mathf.Clamp(startIndex, 0, data.lines.Length - 1);

        panel.SetActive(true);
        optionsPanel.SetActive(false);

        ShowLine();
    }

    public void ForceCloseDialogue()
    {
        EndDialogue();
    }

    void ShowLine()
    {
        if (currentData == null || currentData.lines == null || currentData.lines.Length == 0)
        {
            EndDialogue();
            return;
        }

        if (index >= currentData.lines.Length)
        {
            ShowOptionsOrEnd();
            return;
        }

        var line = currentData.lines[index];

        //jump for previous dialogue
        if (line.jumpBackToPreviousDialogue && hasPreviousDialogue && previousData != null)
        {
            hasPreviousDialogue = false;
            StartDialogue(previousData, npcRef, line.previousDialogueLineIndex);
            return;
        }

        //lane of jumping dialogue
        if (line.showOptionsHere)
        {
            nameText.text = line.speakerName;

            StopAllCoroutines();
            StartCoroutine(TypeText(line.text));
            StartCoroutine(WaitForLineEnd(line));
            return;
        }

        //text
        nameText.text = line.speakerName;

        //font each lane
        if (line.customFont !=null)
        {
            dialogueText.font = line.customFont;
        }

        //right portrait sprite
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

        //left portrait sprite
        if (leftPortrait != null)
        {
            if (line.leftPortrait != null)
                leftPortrait.sprite = line.leftPortrait;
        }

        //ilumination system for the images
        if (line.isLeftSpeaker)
        {
            //left is speaking
            leftPortrait.color = new Color(1f, 1f, 1f, 1f);       //left lighted
            speakerImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); //right darker
        }
        else
        {
            //right is speaking
            leftPortrait.color = new Color(0.5f, 0.5f, 0.5f, 1f);  //left darker
            speakerImage.color = new Color(1f, 1f, 1f, 1f);       //right lighted
        }

        //audio
        if (audioSource != null)
        {
            audioSource.Stop();
            if (line.voiceClip != null)
                audioSource.PlayOneShot(line.voiceClip);
        }

        //typing speed
        currentTypingSpeed = line.typingSpeed;

        //typing coroutines
        StopAllCoroutines();
        StartCoroutine(TypeText(line.text));
        StartCoroutine(WaitForLineEnd(line));
    }

    IEnumerator TypeText(string text)
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

    IEnumerator WaitForLineEnd(DialogueLine line)
    {
        while (isTyping)
            yield return null;

        //jump dialogue
        if (line.jumpBackToPreviousDialogue && hasPreviousDialogue && previousData != null)
        {
            hasPreviousDialogue = false;
            StartDialogue(previousData, npcRef, line.previousDialogueLineIndex);
            yield break;
        }

        //show options after lane finish
        if (line.showOptionsHere)
        {
            ShowOptions();
            yield break;
        }
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

    void ShowOptionsOrEnd()
    {
        if (currentData.options != null && currentData.options.Length > 0)
        {
            ShowOptions();
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowOptions()
    {
        optionsActive = true;
        nextButton.gameObject.SetActive(false);
        optionsPanel.SetActive(true);

        foreach (Transform child in optionsPanel.transform)
            Destroy(child.gameObject);

        foreach (var option in currentData.options)
        {
            Button btn = Instantiate(optionButtonPrefab, optionsPanel.transform);
            btn.GetComponentInChildren<TMP_Text>().text = option.optionText;

            btn.onClick.AddListener(() =>
            {
                optionsActive = false;
                optionsPanel.SetActive(false);
                nextButton.gameObject.SetActive(true);

                previousData = currentData;
                hasPreviousDialogue = true;

                if (option.nextDialogue != null)
                {
                    StartDialogue(option.nextDialogue, npcRef, 0);
                    return;
                }

                EndDialogue();
            });
        }
    }

    void EndDialogue()
    {
        panel.SetActive(false);
        currentData = null;

        if (npcRef != null)
            npcRef.RestoreMovement();

        //Free the selected npc
        NPCDialogue.activeNPC = null;

        npcRef = null;
        hasPreviousDialogue = false;
        previousData = null;
    }
}
