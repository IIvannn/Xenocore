using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;

    [Header("Dialogue Options (optional)")]
    public DialogueOption[] options; //ramification of the dialogue

    [Header("Dialogue End")]
    public bool isFinalDialogue = false;
}