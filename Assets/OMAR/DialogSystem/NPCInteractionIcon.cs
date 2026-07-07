using UnityEngine;

public class NPCInteractionIcon : MonoBehaviour
{
    public GameObject iconFar;   //icon far
    public GameObject iconNear;  //icon if the player is near

    public float detectionRange = 4f; //distance detection for the player
    public float talkRange = 2f;      //distance to talk

    Transform player;
    DialogueUI dialogueUI;

    //npc reference
    NPCDialogue npc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //fix dialogue ui finding
        dialogueUI = FindAnyObjectByType<DialogueUI>(FindObjectsInactive.Include);

        //npc asociated
        npc = GetComponent<NPCDialogue>();

        iconFar.SetActive(true);
        iconNear.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        //if already interacted remove icons
        if (npc != null && npc.HasInteracted)
        {
            iconFar.SetActive(false);
            iconNear.SetActive(false);
            return;
        }

        float dist = Vector3.Distance(player.position, transform.position);

        //if dialogue is open don't show the icons
        if (dialogueUI != null && (dialogueUI.IsOpen || dialogueUI.OptionsActive))
        {
            iconFar.SetActive(false);
            iconNear.SetActive(false);
            return;
        }

        //show far icon at distance
        if (dist > detectionRange)
        {
            iconFar.SetActive(true);
            iconNear.SetActive(false);
            return;
        }

        if (dist <= detectionRange && dist > talkRange)
        {
            iconFar.SetActive(true);
            iconNear.SetActive(false);
            return;
        }

        if (dist <= talkRange)
        {
            iconFar.SetActive(false);
            iconNear.SetActive(true);
        }
    }
}
