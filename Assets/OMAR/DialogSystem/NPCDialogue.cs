using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    public DialogueData dialogue;
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);
        playerInRange = dist <= interactDistance;

        //close dialogue if its out of range
        if (dialogueUI != null && dialogueUI.IsOpen && dist > autoCloseDistance)
        {
            dialogueUI.ForceCloseDialogue();
            RestoreMovement();
            return;
        }

        if (!playerInRange) return;

        //open dialogue with f
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (dialogueUI.IsOpen)
                return;

            dialogueUI.StartDialogue(dialogue, this);
            dialogueJustOpened = true;

            if (blockPlayerMovement)
                DisableMovement();
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
