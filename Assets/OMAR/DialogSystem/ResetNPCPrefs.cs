using UnityEngine;

public class ResetNPCPrefs : MonoBehaviour
{
    public string npcID = "NPC_001";

    void Start()
    {
        PlayerPrefs.DeleteKey(npcID + "_State");
        PlayerPrefs.Save();
        Debug.Log("NPC State Reset");
    }
}