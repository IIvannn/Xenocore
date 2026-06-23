using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC ID")]
    public string npcID = "NPC_001";

    [Header("Dialogue States")]
    public DialogueData[] dialogues; //ist of dialogues

    private int dialogueState = 0; //state on dialogues

    public DialogueUI dialogueUI;

    [Header("Interaction Settings")]
    public float interactDistance = 3f;
    public float autoCloseDistance = 5f;

    [Header("Player Control")]
    public bool blockPlayerMovement = true;

    private Transform player;
    private PlayerMovement playerMovement;
    private bool playerInRange;
    private bool dialogueJustOpened = false;

    void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey(npcID + "_State");
#endif

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();

        dialogueState = PlayerPrefs.GetInt(npcID + "_State", 0);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);
        playerInRange = dist <= interactDistance;

        if (dialogueUI != null && dialogueUI.IsOpen && dist > autoCloseDistance)
        {
            dialogueUI.ForceCloseDialogue();
            RestoreMovement();
            return;
        }

        if (!playerInRange) return;

        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (dialogueUI.IsOpen)
                return;

            DialogueData selected = dialogues[Mathf.Clamp(dialogueState, 0, dialogues.Length - 1)];

            //index for dialogue
            dialogueUI.StartDialogue(selected, this, 0);

            dialogueJustOpened = true;

            if (blockPlayerMovement)
                DisableMovement();

            dialogueState++;
            PlayerPrefs.SetInt(npcID + "_State", dialogueState);
            PlayerPrefs.Save();
        }
    }

    public bool ConsumeJustOpenedFlag()
    {
        bool flag = dialogueJustOpened;
        dialogueJustOpened = false;
        return flag;
    }

    public void RestoreMovement()
    {
        if (playerMovement != null)
            playerMovement.enabled = true;
    }

    private void DisableMovement()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;
    }
}
