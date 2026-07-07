using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC ID")]
    public string npcID = "NPC_001";

    [Header("Dialogue States")]
    public DialogueData[] dialogues; //list of dialogues

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

    public static NPCDialogue activeNPC = null;

    // bloq interaction for loading scene
    private bool hasInteracted = false;
    public bool HasInteracted => hasInteracted; //interaction icon disabled

    void Start()
    {
//#if UNITY_EDITOR
  //      PlayerPrefs.DeleteKey(npcID + "_State");
//#endif

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();

        //load state of npc
        dialogueState = PlayerPrefs.GetInt(npcID + "_State", 0);
    }

    void Update()
    {
        if (player == null) return;

        //if already talked disable it
        if (hasInteracted) return;

        //Bloq dialogue of other npc to appear
        if (NPCDialogue.activeNPC != null && NPCDialogue.activeNPC != this)
            return;

        float dist = Vector3.Distance(player.position, transform.position);
        playerInRange = dist <= interactDistance;

        if (dialogueUI != null && dialogueUI.IsOpen && dist > autoCloseDistance)
        {
            dialogueUI.ForceCloseDialogue();
            RestoreMovement();
            return;
        }

        if (!playerInRange) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            //if there is open a dialogue dont open the other npcs
            if (dialogueUI.IsOpen)
                return;

            DialogueData selected = dialogues[Mathf.Clamp(dialogueState, 0, dialogues.Length - 1)];
            dialogueUI.StartDialogue(selected, this, 0);

            //Active NPC only this dialogue
            NPCDialogue.activeNPC = this;

            dialogueJustOpened = true;

            if (blockPlayerMovement)
                DisableMovement();

            //already interacted
            hasInteracted = true;

            //state and save it
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
